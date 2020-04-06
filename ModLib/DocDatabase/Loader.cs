using ModLib.Debug;
using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace ModLib
{
    public static class Loader
    {
        private static readonly string LoadablesFolderName = "Loadables";
        public static Dictionary<Type, Dictionary<string, ILoadable>> Data { get; } = new Dictionary<Type, Dictionary<string, ILoadable>>();

        /// <summary>
        /// Returns the ILoadable of type T with the given ID.
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve</typeparam>
        /// <param name="id">ID of object to retrieve</param>
        /// <returns></returns>
        public static T Get<T>(string id) where T : ILoadable
        {
            //First check if the dictionary contains the key
            if (!Data.ContainsKey(typeof(T)))
                return default;
            if (!Data[typeof(T)].ContainsKey(id))
                return default;

            return (T)Data[typeof(T)][id];
        }

        /// <summary>
        /// Loads all files for the given module.
        /// </summary>
        /// <param name="moduleName">Name of the module to load the files from. This is the name of the actual folder in the Bannerlord Modules folder.</param>
        /// <returns>Returns true if initialisation was successful.</returns>
        public static bool Initialise(string moduleName)
        {
            bool successful = false;
            try
            {
                LoadAllFiles(moduleName);
                successful = true;
            }
            catch (Exception ex)
            {
                ModDebug.ShowError($"An error occurred whilst trying to load files for module: {moduleName}", "Error occurred during loading files", ex);
            }
            return successful;
        }

        public static void CreateFile<T>(string moduleName, string subFolder = "") where T : ILoadable
        {
            try
            {
                string path = Path.Combine(BasePath.Name, "Modules", moduleName);
                if (!Directory.Exists(path))
                    throw new Exception($"Cannot find the module named {moduleName}");
                path = Path.Combine(path, "ModuleData", LoadablesFolderName);
                if (!string.IsNullOrWhiteSpace(subFolder))
                    path = Path.Combine(path, subFolder);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(path, $"{typeof(T).Name}.xml");

                if (File.Exists(path))
                    File.Delete(path);

                using (XmlWriter writer = XmlWriter.Create(path))
                {
                    XmlRootAttribute rootNode = new XmlRootAttribute();
                    rootNode.ElementName = typeof(T).FullName;
                    var serializer = new XmlSerializer(typeof(T), rootNode);
                    ILoadable obj = (ILoadable)Activator.CreateInstance(typeof(T));
                    serializer.Serialize(writer, obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot create the file for {typeof(T).Name} for module {moduleName}:\n\n{ex.ToStringFull()}");
            }
        }

        private static void Add(ILoadable loadable)
        {
            if (loadable == null)
                throw new ArgumentNullException("Tried to add something to the Loader Data dictionary that was null");
            if (string.IsNullOrWhiteSpace(loadable.ID))
                throw new ArgumentNullException($"Loadable of type {loadable.GetType().ToString()} has missing ID field");

            Type type = loadable.GetType();
            if (!Data.ContainsKey(type))
                Data.Add(type, new Dictionary<string, ILoadable>());

            if (Data[type].ContainsKey(loadable.ID))
            {
                ModDebug.LogError($"Loader already contains Type: {type.AssemblyQualifiedName} ID: {loadable.ID}, overriting...");
                Data[type][loadable.ID] = loadable;
            }
            else
                Data[type].Add(loadable.ID, loadable);
        }

        private static void LoadFromFile(string filePath)
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                string nodeData = "";
                try
                {
                    //Find the type name
                    if (reader.MoveToContent() == XmlNodeType.Element)
                        nodeData = reader.Name;
                    //If we couldn't find the type name, throw an exception saying so. If the root node doesn't include the namespace, throw an exception saying so.
                    if (string.IsNullOrWhiteSpace(nodeData))
                        throw new Exception($"Could not find the root node in xml document located at {filePath}");

                    TypeData data = new TypeData(nodeData);
                    //Find the type from the root node name. The root node should be the full name of the type, including the namespace and the assembly.

                    if (data.Type == null)
                        throw new Exception($"Unable to find type {data.FullName}");

                    XmlRootAttribute root = new XmlRootAttribute();
                    root.ElementName = nodeData;
                    root.IsNullable = true;
                    XmlSerializer serialiser = new XmlSerializer(data.Type, root);
                    ILoadable loaded = (ILoadable)serialiser.Deserialize(reader);
                    if (loaded != null)
                        Add(loaded);
                    else
                        throw new Exception($"Unable to load {data.FullName} from file {filePath}.");
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentNullException && ((ArgumentNullException)ex).ParamName == "type")
                        throw new Exception($"Cannot get a type from type name {nodeData} in file {filePath}", ex);
                    throw new Exception($"An error occurred whilst loading file {filePath}", ex);
                }
            }
        }

        /// <summary>
        /// Loads all files in the Loadables folder for the given module.
        /// </summary>
        /// <param name="moduleName">This is the name of the module to load the files for. This is the name of the module folder.</param>
        private static void LoadAllFiles(string moduleName)
        {
            //Check if the given module name is correct
            string modulePath = Path.Combine(BasePath.Name, "Modules", moduleName);
            if (!Directory.Exists(modulePath))
                throw new Exception($"Cannot find module named {moduleName}");
            //Check the module's ModuleData folder for the Loadables folder.
            string moduleLoadablesPath = Path.Combine(modulePath, "ModuleData", LoadablesFolderName);
            if (Directory.Exists(moduleLoadablesPath))
            {
                try
                {
                    //If the module has a Loadables folder, loop through it and load all the files.
                    //Starting with the files in the root folder
                    foreach (var filePath in Directory.GetFiles(moduleLoadablesPath, "*.xml"))
                    {
                        LoadFromFile(filePath);
                    }

                    //Loop through any subfolders and load the files in them
                    string[] subDirs = Directory.GetDirectories(moduleLoadablesPath);
                    if (subDirs.Count() > 0)
                    {
                        foreach (var subDir in subDirs)
                        {
                            foreach (var filePath in Directory.GetFiles(subDir, "*.xml"))
                            {
                                LoadFromFile(filePath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while Loader was trying to load all files for module {moduleName}", ex);
                }
            }
            else
                ModDebug.LogError($@"Tried to load files for mod {moduleName}, but it doesn't contain a ModuleData\Loadables folder");
        }

        private class TypeData
        {
            public string AssemblyName { get; private set; } = "";
            public string TypeName { get; private set; } = "";
            public string FullName => $"{TypeName}, {AssemblyName}";
            public Type Type
            {
                get
                {
                    return Type.GetType(FullName);
                }
            }

            public TypeData(string nodeData)
            {
                if (!string.IsNullOrWhiteSpace(nodeData))
                {
                    if (!nodeData.Contains("-"))
                        throw new ArgumentException($"Node data does not contain an assembly string\nNode Data: {nodeData}");
                    if (!nodeData.Contains("."))
                        throw new ArgumentException($"Node data does not contain a namespace string\nNode Data: {nodeData}");

                    string[] split = nodeData.Split('-');

                    if (!string.IsNullOrWhiteSpace(split[0]))
                        AssemblyName = split[0];
                    else
                        throw new ArgumentException($"Assembly name in node data was null or empty\nNode Data: {nodeData}");

                    if (!string.IsNullOrWhiteSpace(split[1]))
                        TypeName = split[1];
                    else
                        throw new ArgumentException($"Type name in node data was null or empty\nNode Data: {nodeData}");
                }
                else
                    throw new ArgumentException($"The given node data was invalid.\nNode Data: {nodeData}");
            }
        }
    }
}

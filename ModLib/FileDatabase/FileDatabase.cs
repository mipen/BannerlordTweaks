using ModLib.Debugging;
using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using TaleWorlds.Library;

namespace ModLib
{
    public static class FileDatabase
    {
        private static readonly string LoadablesFolderName = "Loadables";
        public static Dictionary<Type, Dictionary<string, ISerialisableFile>> Data { get; } = new Dictionary<Type, Dictionary<string, ISerialisableFile>>();

        /// <summary>
        /// Returns the ILoadable of type T with the given ID.
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve</typeparam>
        /// <param name="id">ID of object to retrieve</param>
        /// <returns></returns>
        public static T Get<T>(string id) where T : ISerialisableFile
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

        /// <summary>
        /// Saves the given instance to file.
        /// </summary>
        /// <typeparam name="T">Type of the instance to save to file.</typeparam>
        /// <param name="moduleName">The folder name of the module to save to.</param>
        /// <param name="sf">Instance of the object to save to file.</param>
        public static bool SaveToFile(string moduleName, ISerialisableFile sf, Location location = Location.Modules)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sf.ID))
                    throw new Exception($"FileDatabase tried to save an object of type {sf.GetType().FullName} but the ID value was null.");
                if (string.IsNullOrWhiteSpace(moduleName))
                    throw new Exception($"FileDatabase tried to save an object of type {sf.GetType().FullName} with ID {sf.ID} but the module folder name given was null or empty.");

                string path = GetPathForModule(moduleName, location);

                if (!Directory.Exists(path))
                    throw new Exception($"FileDatabase cannot find the module named {moduleName}");

                if (location == Location.Modules)
                    path = Path.Combine(path, "ModuleData", LoadablesFolderName);

                if (sf is ISubFolder)
                {
                    ISubFolder subFolder = sf as ISubFolder;
                    if (!string.IsNullOrWhiteSpace(subFolder.SubFolder))
                        path = Path.Combine(path, subFolder.SubFolder);
                }

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = Path.Combine(path, $@"{sf.GetType().Name}.{sf.ID}.xml");

                if (File.Exists(path))
                    File.Delete(path);

                using (XmlWriter writer = XmlWriter.Create(path, new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true }))
                {
                    XmlRootAttribute rootNode = new XmlRootAttribute();
                    rootNode.ElementName = $"{sf.GetType().Assembly.GetName().Name}-{sf.GetType().FullName}";
                    XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                    var serializer = new XmlSerializer(sf.GetType(), rootNode);
                    serializer.Serialize(writer, sf, xmlns);
                }
                return true;
            }
            catch (Exception ex)
            {
                ModDebug.ShowError($"Cannot create the file for type {sf.GetType().FullName} with ID {sf.ID} for module {moduleName}:", "Error saving to file", ex);
                return false;
            }
        }

        private static void Add(ISerialisableFile loadable)
        {
            if (loadable == null)
                throw new ArgumentNullException("Tried to add something to the Loader Data dictionary that was null");
            if (string.IsNullOrWhiteSpace(loadable.ID))
                throw new ArgumentNullException($"Loadable of type {loadable.GetType().ToString()} has missing ID field");

            Type type = loadable.GetType();
            if (!Data.ContainsKey(type))
                Data.Add(type, new Dictionary<string, ISerialisableFile>());

            if (Data[type].ContainsKey(loadable.ID))
            {
                ModDebug.LogError($"Loader already contains Type: {type.AssemblyQualifiedName} ID: {loadable.ID}, overwriting...");
                Data[type][loadable.ID] = loadable;
            }
            else
                Data[type].Add(loadable.ID, loadable);
        }

        private static void LoadFromFile(string filePath)
        {
            //DEBUG:: People can't read and aren't deleting the old mod installation. Need to manually delete the old config file for a couple updates.
            string modulefolder = Directory.GetParent(filePath).Parent.Parent.Name;
            if (Path.GetFileName(filePath) == "config.xml" && modulefolder == "zzBannerlordTweaks")
            {
                File.Delete(filePath);
                return;
            }
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
                    ISerialisableFile loaded = (ISerialisableFile)serialiser.Deserialize(reader);
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
        /// Loads all files in the Loadables folder for the given module and from the Documents folder for the given module.
        /// </summary>
        /// <param name="moduleName">This is the name of the module to load the files for. This is the name of the module folder.</param>
        private static void LoadAllFiles(string moduleName)
        {
            #region Loadables Folder
            //Check if the given module name is correct
            string modulePath = GetPathForModule(moduleName, Location.Modules);
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
                                try
                                {
                                    LoadFromFile(filePath);
                                }
                                catch (Exception ex)
                                {
                                    ModDebug.LogError($"Failed to load file: {filePath} \n\nSkipping..\n\n", ex);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while FileDatabase was trying to load all files for module {moduleName}", ex);
                }
            }
            else
                Directory.CreateDirectory(moduleLoadablesPath);
            #endregion
            #region Documents Folder
            //TODO::
            string modConfigsPath = GetPathForModule(moduleName, Location.Configs);
            if (Directory.Exists(modConfigsPath))
            {
                foreach (string filePath in Directory.GetFiles(modConfigsPath))
                {
                    try
                    {
                        LoadFromFile(filePath);
                    }
                    catch (Exception ex)
                    {
                        ModDebug.LogError($"Failed to load file: {filePath}\n\n Skipping...", ex);
                    }
                }
            }
            else
                Directory.CreateDirectory(modConfigsPath);
            #endregion
        }

        private static string GetPathForModule(string moduleName, Location location)
        {
            if (location == Location.Modules)
                return System.IO.Path.Combine(BasePath.Name, "Modules", moduleName);
            else
                return System.IO.Path.Combine(TaleWorlds.Engine.Utilities.GetConfigsPath(), moduleName);
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

        public enum Location
        {
            Modules,
            Configs
        }
    }
}

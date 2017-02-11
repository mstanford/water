// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005-2007 Swampware, Inc.
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// ------------------------------------------------------------------------------
using System;

namespace Bamboo.VisualStudio.VisualStudio2003.CSharpProject
{
	/// <summary>
	/// Summary description for ProjectReader.
	/// </summary>
	public class ProjectReader
	{

		public static Models.Project Read(string filename)
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);
			string name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
			System.IO.TextReader reader = System.IO.File.OpenText(filename);
			Models.Project project = Read(name, reader);
			reader.Close();
			return project;
		}

		/// <summary>
		/// Reads a project from a file, and returns the project.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public static Models.Project Read(string name, System.IO.TextReader reader)
		{
			Models.Project project = new Models.Project();

			project.Name = name;

			LoadProject(reader, project);

			return project;
		}

		private static void LoadProject(System.IO.TextReader textReader, Models.Project project)
		{
			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.Load(textReader);

			LoadCSharp(xmlDocument, project);
			LoadSettings(xmlDocument, project);
			LoadConfigs(xmlDocument, project);
			LoadReferences(xmlDocument, project);
			LoadFiles(xmlDocument, project);
			LoadFolders(xmlDocument, project);
		}

		private static void LoadCSharp(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("CSHARP") )
			{
				string ProjectType = node.Attributes["ProjectType"].Value;
				string ProductVersion = node.Attributes["ProductVersion"].Value;
				string SchemaVersion = node.Attributes["SchemaVersion"].Value;
				System.Guid ProjectGuid = new System.Guid(node.Attributes["ProjectGuid"].Value);

				project.ProjectType = ProjectType;
				project.ProductVersion = ProductVersion;
				project.SchemaVersion = SchemaVersion;
				project.ProjectGuid = ProjectGuid;
			}
		}

		private static void LoadSettings(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("Settings") )
			{
				string ApplicationIcon = node.Attributes["ApplicationIcon"].Value;
				string AssemblyKeyContainerName = node.Attributes["AssemblyKeyContainerName"].Value;
				string AssemblyName = node.Attributes["AssemblyName"].Value;
				string AssemblyOriginatorKeyFile = node.Attributes["AssemblyOriginatorKeyFile"].Value;
				string DefaultClientScript = node.Attributes["DefaultClientScript"].Value;
				string DefaultHTMLPageLayout = node.Attributes["DefaultHTMLPageLayout"].Value;
				string DefaultTargetSchema = node.Attributes["DefaultTargetSchema"].Value;
				string DelaySign = node.Attributes["DelaySign"].Value;
				string OutputType = node.Attributes["OutputType"].Value;
				string PreBuildEvent = node.Attributes["PreBuildEvent"].Value;
				string PostBuildEvent = node.Attributes["PostBuildEvent"].Value;
				string RootNamespace = node.Attributes["RootNamespace"].Value;
				string RunPostBuildEvent = node.Attributes["RunPostBuildEvent"].Value;
				string StartupObject = node.Attributes["StartupObject"].Value;

				Models.Settings settings = new Models.Settings();
				settings.ApplicationIcon = ApplicationIcon;
				settings.AssemblyKeyContainerName = AssemblyKeyContainerName;
				settings.AssemblyName = AssemblyName;
				settings.AssemblyOriginatorKeyFile = AssemblyOriginatorKeyFile;
				settings.DefaultClientScript = DefaultClientScript;
				settings.DefaultHTMLPageLayout = DefaultHTMLPageLayout;
				settings.DefaultTargetSchema = DefaultTargetSchema;
				settings.DelaySign = DelaySign;
				settings.OutputType = OutputType;
				settings.PreBuildEvent = PreBuildEvent;
				settings.PostBuildEvent = PostBuildEvent;
				settings.RootNamespace = RootNamespace;
				settings.RunPostBuildEvent = RunPostBuildEvent;
				settings.StartupObject = StartupObject;
				project.Settings = settings;
			}
		}

		private static void LoadConfigs(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("Config") )
			{
				string Name = node.Attributes["Name"].Value;
				string AllowUnsafeBlocks = node.Attributes["AllowUnsafeBlocks"].Value;
				string BaseAddress = node.Attributes["BaseAddress"].Value;
				string CheckForOverflowUnderflow = node.Attributes["CheckForOverflowUnderflow"].Value;
				string ConfigurationOverrideFile = node.Attributes["ConfigurationOverrideFile"].Value;
				string DefineConstants = node.Attributes["DefineConstants"].Value;
				string DocumentationFile = node.Attributes["DocumentationFile"].Value;
				string DebugSymbols = node.Attributes["DebugSymbols"].Value;
				string FileAlignment = node.Attributes["FileAlignment"].Value;
				string IncrementalBuild = node.Attributes["IncrementalBuild"].Value;
				string NoStdLib = node.Attributes["NoStdLib"].Value;
				string NoWarn = node.Attributes["NoWarn"].Value;
				string Optimize = node.Attributes["Optimize"].Value;
				string OutputPath = node.Attributes["OutputPath"].Value;
				string RegisterForComInterop = node.Attributes["RegisterForComInterop"].Value;
				string RemoveIntegerChecks = node.Attributes["RemoveIntegerChecks"].Value;
				string TreatWarningsAsErrors = node.Attributes["TreatWarningsAsErrors"].Value;
				string WarningLevel = node.Attributes["WarningLevel"].Value;

				Models.Config config = new Models.Config();
				config.Name = Name;
				config.AllowUnsafeBlocks = AllowUnsafeBlocks;
				config.BaseAddress = BaseAddress;
				config.CheckForOverflowUnderflow = CheckForOverflowUnderflow;
				config.ConfigurationOverrideFile = ConfigurationOverrideFile;
				config.DefineConstants = DefineConstants;
				config.DocumentationFile = DocumentationFile;
				config.DebugSymbols = DebugSymbols;
				config.FileAlignment = FileAlignment;
				config.IncrementalBuild = IncrementalBuild;
				config.NoStdLib = NoStdLib;
				config.NoWarn = NoWarn;
				config.Optimize = Optimize;
				config.OutputPath = OutputPath;
				config.RegisterForComInterop = RegisterForComInterop;
				config.RemoveIntegerChecks = RemoveIntegerChecks;
				config.TreatWarningsAsErrors = TreatWarningsAsErrors;
				config.WarningLevel = WarningLevel;
				project.Configs.Add(config);
			}
		}

		private static void LoadReferences(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("Reference") )
			{
				Models.Reference reference = new Models.Reference();

				foreach(System.Xml.XmlAttribute attribute in node.Attributes)
				{
					switch(attribute.LocalName)
					{
						case "Name" :
						{
							reference.Name = node.Attributes["Name"].Value;
							break;
						}
						case "AssemblyName" :
						{
							reference.AssemblyName = node.Attributes["AssemblyName"].Value;
							break;
						}
						case "HintPath" :
						{
							reference.HintPath = node.Attributes["HintPath"].Value;
							break;
						}
						case "Project" :
						{
							reference.Project = node.Attributes["Project"].Value;
							break;
						}
						case "Package" :
						{
							reference.Package = node.Attributes["Package"].Value;
							break;
						}
						case "Guid" :
						{
							reference.Guid = node.Attributes["Guid"].Value;
							break;
						}
						case "VersionMajor" :
						{
							reference.VersionMajor = node.Attributes["VersionMajor"].Value;
							break;
						}
						case "VersionMinor" :
						{
							reference.VersionMinor = node.Attributes["VersionMinor"].Value;
							break;
						}
						case "Lcid" :
						{
							reference.Lcid = node.Attributes["Lcid"].Value;
							break;
						}
						case "WrapperTool" :
						{
							reference.WrapperTool = node.Attributes["WrapperTool"].Value;
							break;
						}
						case "Private" :
						{
							reference.Private = node.Attributes["Private"].Value;
							break;
						}
						case "AssemblyFolderKey" :
						{
							reference.AssemblyFolderKey = node.Attributes["AssemblyFolderKey"].Value;
							break;
						}
						default :
						{
							throw new System.Exception("Unknown project reference attribute: " + attribute.LocalName);
						}
					}
				}

				project.References.Add(reference);
			}
		}

		private static void LoadFiles(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("File") )
			{
				Models.Item item = new Models.Item();

				foreach(System.Xml.XmlAttribute attribute in node.Attributes)
				{
					switch(attribute.LocalName)
					{
						case "RelPath" :
						{
							item.RelPath = node.Attributes["RelPath"].Value;
							break;
						}
						case "SubType" :
						{
							item.SubType = node.Attributes["SubType"].Value;
							break;
						}
						case "DependentUpon" :
						{
							item.DependentUpon = node.Attributes["DependentUpon"].Value;
							break;
						}
						case "BuildAction" :
						{
							item.BuildAction = node.Attributes["BuildAction"].Value;
							break;
						}
						case "Generator" :
						{
							item.Generator = node.Attributes["Generator"].Value;
							break;
						}
						case "LastGenOutput" :
						{
							item.LastGenOutput = node.Attributes["LastGenOutput"].Value;
							break;
						}
						case "DesignTime" :
						{
							item.DesignTime = node.Attributes["DesignTime"].Value;
							break;
						}
						case "AutoGen" :
						{
							item.AutoGen = node.Attributes["AutoGen"].Value;
							break;
						}
						default :
						{
							throw new System.Exception("Unknown project file attribute: " + attribute.LocalName);
						}
					}
				}

				project.Items.Add(item);
			}
		}

		private static void LoadFolders(System.Xml.XmlDocument xmlDocument, Models.Project project)
		{
			foreach( System.Xml.XmlNode node in xmlDocument.GetElementsByTagName("Folder") )
			{
				Models.Folder folder = new Models.Folder();

				foreach(System.Xml.XmlAttribute attribute in node.Attributes)
				{
					switch(attribute.LocalName)
					{
						case "RelPath" :
						{
							folder.RelPath = node.Attributes["RelPath"].Value;
							break;
						}
						default :
						{
							throw new System.Exception("Unknown project folder attribute: " + attribute.LocalName);
						}
					}
				}

				project.Folders.Add(folder);
			}
		}

		private ProjectReader()
		{
		}

	}
}

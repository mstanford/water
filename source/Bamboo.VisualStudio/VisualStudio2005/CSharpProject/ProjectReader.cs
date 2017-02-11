// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2007 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2005.CSharpProject
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

			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.Load(reader);

			foreach(System.Xml.XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
			{
				switch(xmlNode.LocalName)
				{
					case "PropertyGroup" :
					{
						ReadPropertyGroup(project, (System.Xml.XmlElement)xmlNode);
						break;
					}
					case "ItemGroup" :
					{
						ReadItemGroup(project, (System.Xml.XmlElement)xmlNode);
						break;
					}
					case "Import" :
					{
						ReadImport(project, (System.Xml.XmlElement)xmlNode);
						break;
					}
					default :
					{
						break;
					}
				}
			}

			return project;
		}

		public static void ReadPropertyGroup(Models.Project project, System.Xml.XmlElement element)
		{
			if(element.Attributes.Count == 0)
			{
				foreach(System.Xml.XmlElement element2 in element.ChildNodes)
				{
					switch(element2.LocalName)
					{
						case "Configuration" :
						{
							project.Settings.Configuration = element2.InnerText;
							project.Settings.ConfigurationCondition = element2.Attributes["Condition"].Value;
							break;
						}
						case "Platform" :
						{
							project.Settings.Platform = element2.InnerText;
							project.Settings.PlatformCondition = element2.Attributes["Condition"].Value;
							break;
						}
						case "ProductVersion" :
						{
							project.Settings.ProductVersion = element2.InnerText;
							break;
						}
						case "SchemaVersion" :
						{
							project.Settings.SchemaVersion = element2.InnerText;
							break;
						}
						case "ProjectGuid" :
						{
							project.ProjectGuid = new System.Guid(element2.InnerText.Substring(1, element2.InnerText.Length - 2));
							break;
						}
						case "OutputType" :
						{
							project.Settings.OutputType = element2.InnerText;
							break;
						}
						case "AppDesignerFolder" :
						{
							project.Settings.AppDesignerFolder = element2.InnerText;
							break;
						}
						case "RootNamespace" :
						{
							project.Settings.RootNamespace = element2.InnerText;
							break;
						}
						case "AssemblyName" :
						{
							project.Settings.AssemblyName = element2.InnerText;
							break;
						}
						case "IsWebBootstrapper" :
						{
							project.Settings.IsWebBootstrapper = element2.InnerText;
							break;
						}
						case "SignManifests" :
						{
							project.Settings.SignManifests = element2.InnerText;
							break;
						}
						case "GenerateManifests" :
						{
							project.Settings.GenerateManifests = element2.InnerText;
							break;
						}
						case "ManifestCertificateThumbprint" :
						{
							project.Settings.ManifestCertificateThumbprint = element2.InnerText;
							break;
						}
						case "ManifestKeyFile" :
						{
							project.Settings.ManifestKeyFile = element2.InnerText;
							break;
						}
						case "PublishUrl" :
						{
							project.Settings.PublishUrl = element2.InnerText;
							break;
						}
						case "Install" :
						{
							project.Settings.Install = element2.InnerText;
							break;
						}
						case "InstallFrom" :
						{
							project.Settings.InstallFrom = element2.InnerText;
							break;
						}
						case "UpdateEnabled" :
						{
							project.Settings.UpdateEnabled = element2.InnerText;
							break;
						}
						case "UpdateMode" :
						{
							project.Settings.UpdateMode = element2.InnerText;
							break;
						}
						case "UpdateInterval" :
						{
							project.Settings.UpdateInterval = element2.InnerText;
							break;
						}
						case "UpdateIntervalUnits" :
						{
							project.Settings.UpdateIntervalUnits = element2.InnerText;
							break;
						}
						case "UpdatePeriodically" :
						{
							project.Settings.UpdatePeriodically = element2.InnerText;
							break;
						}
						case "UpdateRequired" :
						{
							project.Settings.UpdateRequired = element2.InnerText;
							break;
						}
						case "MapFileExtensions" :
						{
							project.Settings.MapFileExtensions = element2.InnerText;
							break;
						}
						case "ProductName" :
						{
							project.Settings.ProductName = element2.InnerText;
							break;
						}
						case "PublisherName" :
						{
							project.Settings.PublisherName = element2.InnerText;
							break;
						}
						case "ApplicationVersion" :
						{
							project.Settings.ApplicationVersion = element2.InnerText;
							break;
						}
						case "BootstrapperEnabled" :
						{
							project.Settings.BootstrapperEnabled = element2.InnerText;
							break;
						}
						case "StartupObject" :
						{
							//TODO Implement.
							break;
						}
						case "ApplicationIcon" :
						{
							project.Settings.ApplicationIcon = element2.InnerText;
							break;
						}
						case "ProjectType" :
						{
							//TODO Implement.
							break;
						}
						case "AssemblyKeyContainerName" :
						{
							//TODO Implement.
							break;
						}
						case "AssemblyOriginatorKeyFile" :
						{
							//TODO Implement.
							break;
						}
						case "DefaultClientScript" :
						{
							//TODO Implement.
							break;
						}
						case "DefaultHTMLPageLayout" :
						{
							//TODO Implement.
							break;
						}
						case "DefaultTargetSchema" :
						{
							//TODO Implement.
							break;
						}
						case "DelaySign" :
						{
							//TODO Implement.
							break;
						}
						case "RunPostBuildEvent" :
						{
							//TODO Implement.
							break;
						}
						case "FileUpgradeFlags" :
						{
							//TODO Implement.
							break;
						}
						case "UpgradeBackupLocation" :
						{
							//TODO Implement.
							break;
						}
						case "PreBuildEvent" :
						{
							//TODO Implement.
							break;
						}
						case "PostBuildEvent" :
						{
							//TODO Implement.
							break;
						}
						default :
						{
//TODO                            throw new System.Exception("Unrecognized PropertyGroup node: " + element2.LocalName + " = " + element2.InnerText);
							break;
						}
					}
				}
			}
			else
			{
				ReadConfiguration(project, element);
			}
		}

		public static void ReadConfiguration(Models.Project project, System.Xml.XmlElement element)
		{
			Models.Config config = new Models.Config();
			config.Condition = element.Attributes["Condition"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "DebugSymbols" :
					{
						config.DebugSymbols = element2.InnerText;
						break;
					}
					case "DebugType" :
					{
						config.DebugType = element2.InnerText;
						break;
					}
					case "Optimize" :
					{
						config.Optimize = element2.InnerText;
						break;
					}
					case "OutputPath" :
					{
						config.OutputPath = element2.InnerText;
						break;
					}
					case "DefineConstants" :
					{
						config.DefineConstants = element2.InnerText;
						break;
					}
					case "ErrorReport" :
					{
						config.ErrorReport = element2.InnerText;
						break;
					}
					case "WarningLevel" :
					{
						config.WarningLevel = element2.InnerText;
						break;
					}
					case "AllowUnsafeBlocks" :
					{
                        //TODO Implement.
                        break;
					}
					case "BaseAddress" :
					{
                        //TODO Implement.
                        break;
					}
					case "CheckForOverflowUnderflow" :
					{
                        //TODO Implement.
                        break;
					}
					case "ConfigurationOverrideFile" :
					{
                        //TODO Implement.
                        break;
					}
					case "DocumentationFile" :
					{
                        //TODO Implement.
                        break;
					}
					case "FileAlignment" :
					{
                        //TODO Implement.
                        break;
					}
					case "NoStdLib" :
					{
                        //TODO Implement.
                        break;
					}
					case "NoWarn" :
					{
                        //TODO Implement.
                        break;
					}
					case "RegisterForComInterop" :
					{
                        //TODO Implement.
                        break;
					}
					case "RemoveIntegerChecks" :
					{
                        //TODO Implement.
                        break;
					}
					case "TreatWarningsAsErrors" :
					{
                        //TODO Implement.
                        break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized PropertyGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.Configs.Add(config);
		}

		public static void ReadItemGroup(Models.Project project, System.Xml.XmlElement element)
		{
			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "Reference" :
					{
						ReadReference(project, element2);
						break;
					}
					case "Compile" :
					case "EmbeddedResource" :
					case "None" :
					case "Content" :
					{
						ReadItem(project, element2);
						break;
					}
					case "Folder" :
					{
						ReadFolder(project, element2);
						break;
					}
					case "BootstrapperFile" :
					{
						ReadBootstrapperFile(project, element2);
						break;
					}
					case "BootstrapperPackage" :
					{
						ReadBootstrapperPackage(project, element2);
						break;
					}
					case "ProjectReference" :
					{
						ReadProjectReference(project, element2);
						break;
					}
					case "Service" :
					{
						//TODO Implement.
						break;
					}
					case "COMReference" :
					{
						//TODO Implement.
						break;
					}
					case "Component" :
					{
						//TODO Implement.
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized PropertyGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}
		}

		public static void ReadReference(Models.Project project, System.Xml.XmlElement element)
		{
			Models.Reference reference = new Models.Reference();
			reference.Include = element.Attributes["Include"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "HintPath" :
					{
						reference.HintPath = element2.InnerText;
						break;
					}
					case "SpecificVersion" :
					{
						reference.SpecificVersion = element2.InnerText;
						break;
					}
					case "Name" :
					{
						reference.Name = element2.InnerText;
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized ItemGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.References.Add(reference);
		}

		public static void ReadItem(Models.Project project, System.Xml.XmlElement element)
		{
			Models.Item item = new Models.Item();
			item.Type = element.LocalName;
			item.Include = element.Attributes["Include"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "SubType" :
					{
						item.SubType = element2.InnerText;
						break;
					}
					case "AutoGen" :
					{
						item.AutoGen = element2.InnerText;
						break;
					}
					case "DependentUpon" :
					{
						item.DependentUpon = element2.InnerText;
						break;
					}
					case "DesignTimeSharedInput" :
					{
						item.DesignTimeSharedInput = element2.InnerText;
						break;
					}
					case "Generator" :
					{
						item.Generator = element2.InnerText;
						break;
					}
					case "LastGenOutput" :
					{
						item.LastGenOutput = element2.InnerText;
						break;
					}
					case "DesignTime" :
					{
						item.DesignTime = element2.InnerText;
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized ItemGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.Items.Add(item);
		}

		public static void ReadFolder(Models.Project project, System.Xml.XmlElement element)
		{
			Models.Folder folder = new Models.Folder();
			folder.Include = element.Attributes["Include"].Value;
			project.Folders.Add(folder);
		}

		public static void ReadBootstrapperFile(Models.Project project, System.Xml.XmlElement element)
		{
			Models.BootstrapperFile bootstrapperFile = new Models.BootstrapperFile();
			bootstrapperFile.Include = element.Attributes["Include"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "InProject" :
					{
						bootstrapperFile.InProject = element2.InnerText;
						break;
					}
					case "ProductName" :
					{
						bootstrapperFile.ProductName = element2.InnerText;
						break;
					}
					case "Install" :
					{
						bootstrapperFile.Install = element2.InnerText;
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized ItemGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.BootstrapperFile = bootstrapperFile;
		}

		public static void ReadBootstrapperPackage(Models.Project project, System.Xml.XmlElement element)
		{
			Models.BootstrapperPackage bootstrapperPackage = new Models.BootstrapperPackage();
			bootstrapperPackage.Include = element.Attributes["Include"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "InProject" :
					{
						bootstrapperPackage.InProject = element2.InnerText;
						break;
					}
					case "ProductName" :
					{
						bootstrapperPackage.ProductName = element2.InnerText;
						break;
					}
					case "Install" :
					{
						bootstrapperPackage.Install = element2.InnerText;
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized ItemGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.BootstrapperPackage = bootstrapperPackage;
		}

		public static void ReadProjectReference(Models.Project project, System.Xml.XmlElement element)
		{
			Models.ProjectReference projectReference = new Models.ProjectReference();
			projectReference.Include = element.Attributes["Include"].Value;

			foreach(System.Xml.XmlElement element2 in element.ChildNodes)
			{
				switch(element2.LocalName)
				{
					case "Project" :
					{
						projectReference.Project = element2.InnerText;
						break;
					}
					case "Name" :
					{
						projectReference.Name = element2.InnerText;
						break;
					}
					case "Package" :
					{
                        //TODO Implement
						break;
					}
					default :
					{
//TODO                        throw new System.Exception("Unrecognized ItemGroup node: " + element2.LocalName + " = " + element2.InnerText);
						break;
					}
				}
			}

			project.ProjectReferences.Add(projectReference);
		}

		public static void ReadImport(Models.Project project, System.Xml.XmlElement element)
		{
			Models.Import import = new Models.Import();
			import.Project = element.Attributes["Project"].Value;
			if(project.Import != null)
			{
				throw new System.Exception("Import already exists.");
			}
			project.Import = import;
		}

		private ProjectReader()
		{
		}

	}
}

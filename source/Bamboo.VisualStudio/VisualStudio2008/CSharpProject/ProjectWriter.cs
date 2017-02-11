// ------------------------------------------------------------------------------
// 
// Copyright (c) 2006-2008 Swampware, Inc.
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
using System.Collections.Generic;

namespace Bamboo.VisualStudio.VisualStudio2008.CSharpProject
{
	/// <summary>
	/// Summary description for ProjectWriter.
	/// </summary>
	public class ProjectWriter
	{

		public static void Write(Models.Project project, System.IO.TextWriter writer)
		{
			writer.WriteLine("<Project DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\" ToolsVersion=\"3.5\">");

			writer.WriteLine("  <PropertyGroup>");
			writer.WriteLine("    <Configuration Condition=\"" + project.Settings.ConfigurationCondition + "\">" + project.Settings.Configuration + "</Configuration>");
			writer.WriteLine("    <Platform Condition=\"" + project.Settings.PlatformCondition + "\">" + project.Settings.Platform + "</Platform>");
			writer.WriteLine("    <ProductVersion>" + project.Settings.ProductVersion + "</ProductVersion>");
			writer.WriteLine("    <SchemaVersion>" + project.Settings.SchemaVersion + "</SchemaVersion>");
			writer.WriteLine("    <ProjectGuid>{" + project.ProjectGuid.ToString().ToUpper() + "}</ProjectGuid>");
			writer.WriteLine("    <OutputType>" + project.Settings.OutputType + "</OutputType>");
			writer.WriteLine("    <AppDesignerFolder>" + project.Settings.AppDesignerFolder + "</AppDesignerFolder>");
			writer.WriteLine("    <RootNamespace>" + project.Settings.RootNamespace + "</RootNamespace>");
			writer.WriteLine("    <AssemblyName>" + project.Settings.AssemblyName + "</AssemblyName>");
			writer.WriteLine("    <FileUpgradeFlags>");
			writer.WriteLine("    </FileUpgradeFlags>");
			writer.WriteLine("    <UpgradeBackupLocation>");
			writer.WriteLine("    </UpgradeBackupLocation>");
			if (project.Settings.OldToolsVersion.Length > 0)
			{
				writer.WriteLine("    <OldToolsVersion>" + project.Settings.OldToolsVersion + "</OldToolsVersion>");
			}

			if (project.Settings.ApplicationIcon.Length > 0)
			{
				writer.WriteLine("    <ApplicationIcon>" + project.Settings.ApplicationIcon + "</ApplicationIcon>");
			}
			if (project.Settings.IsWebBootstrapper.Length > 0)
			{
				writer.WriteLine("    <IsWebBootstrapper>" + project.Settings.IsWebBootstrapper + "</IsWebBootstrapper>");
			}
			if(project.Settings.SignManifests.Length > 0)
			{
				writer.WriteLine("    <SignManifests>" + project.Settings.SignManifests + "</SignManifests>");
			}
			if(project.Settings.GenerateManifests.Length > 0)
			{
				writer.WriteLine("    <GenerateManifests>" + project.Settings.GenerateManifests + "</GenerateManifests>");
			}
			if(project.Settings.ManifestCertificateThumbprint.Length > 0)
			{
				writer.WriteLine("    <ManifestCertificateThumbprint>" + project.Settings.ManifestCertificateThumbprint + "</ManifestCertificateThumbprint>");
			}
			if(project.Settings.ManifestKeyFile.Length > 0)
			{
				writer.WriteLine("    <ManifestKeyFile>" + project.Settings.ManifestKeyFile + "</ManifestKeyFile>");
			}
			if(project.Settings.PublishUrl.Length > 0)
			{
				writer.WriteLine("    <PublishUrl>" + project.Settings.PublishUrl + "</PublishUrl>");
			}
			if(project.Settings.Install.Length > 0)
			{
				writer.WriteLine("    <Install>" + project.Settings.Install + "</Install>");
			}
			if(project.Settings.InstallFrom.Length > 0)
			{
				writer.WriteLine("    <InstallFrom>" + project.Settings.InstallFrom + "</InstallFrom>");
			}
			if(project.Settings.UpdateEnabled.Length > 0)
			{
				writer.WriteLine("    <UpdateEnabled>" + project.Settings.UpdateEnabled + "</UpdateEnabled>");
			}
			if(project.Settings.UpdateMode.Length > 0)
			{
				writer.WriteLine("    <UpdateMode>" + project.Settings.UpdateMode + "</UpdateMode>");
			}
			if(project.Settings.UpdateInterval.Length > 0)
			{
				writer.WriteLine("    <UpdateInterval>" + project.Settings.UpdateInterval + "</UpdateInterval>");
			}
			if(project.Settings.UpdateIntervalUnits.Length > 0)
			{
				writer.WriteLine("    <UpdateIntervalUnits>" + project.Settings.UpdateIntervalUnits + "</UpdateIntervalUnits>");
			}
			if(project.Settings.UpdatePeriodically.Length > 0)
			{
				writer.WriteLine("    <UpdatePeriodically>" + project.Settings.UpdatePeriodically + "</UpdatePeriodically>");
			}
			if(project.Settings.UpdateRequired.Length > 0)
			{
				writer.WriteLine("    <UpdateRequired>" + project.Settings.UpdateRequired + "</UpdateRequired>");
			}
			if(project.Settings.MapFileExtensions.Length > 0)
			{
				writer.WriteLine("    <MapFileExtensions>" + project.Settings.MapFileExtensions + "</MapFileExtensions>");
			}
			if(project.Settings.ProductName.Length > 0)
			{
				writer.WriteLine("    <ProductName>" + project.Settings.ProductName + "</ProductName>");
			}
			if(project.Settings.PublisherName.Length > 0)
			{
				writer.WriteLine("    <PublisherName>" + project.Settings.PublisherName + "</PublisherName>");
			}
			if(project.Settings.ApplicationVersion.Length > 0)
			{
				writer.WriteLine("    <ApplicationVersion>" + project.Settings.ApplicationVersion + "</ApplicationVersion>");
			}
			if(project.Settings.BootstrapperEnabled.Length > 0)
			{
				writer.WriteLine("    <BootstrapperEnabled>" + project.Settings.BootstrapperEnabled + "</BootstrapperEnabled>");
			}

			writer.WriteLine("  </PropertyGroup>");

			foreach(Models.Config config in project.Configs)
			{
				writer.WriteLine("  <PropertyGroup Condition=\"" + config.Condition + "\">");
				if(config.DebugSymbols.Length > 0)
				{
					writer.WriteLine("    <DebugSymbols>" + config.DebugSymbols + "</DebugSymbols>");
				}
				if(config.DebugType.Length > 0)
				{
					writer.WriteLine("    <DebugType>" + config.DebugType + "</DebugType>");
				}
				if(config.Optimize.Length > 0)
				{
					writer.WriteLine("    <Optimize>" + config.Optimize + "</Optimize>");
				}
				if(config.OutputPath.Length > 0)
				{
					writer.WriteLine("    <OutputPath>" + config.OutputPath + "</OutputPath>");
				}
				if(config.DefineConstants.Length > 0)
				{
					writer.WriteLine("    <DefineConstants>" + config.DefineConstants + "</DefineConstants>");
				}
				if(config.ErrorReport.Length > 0)
				{
					writer.WriteLine("    <ErrorReport>" + config.ErrorReport + "</ErrorReport>");
				}
				if(config.WarningLevel.Length > 0)
				{
					writer.WriteLine("    <WarningLevel>" + config.WarningLevel + "</WarningLevel>");
				}
				writer.WriteLine("  </PropertyGroup>");
			}

			writer.WriteLine("  <ItemGroup>");
			foreach(Models.Reference reference in project.References)
			{
				if(reference.HintPath.Length == 0 && reference.SpecificVersion.Length == 0)
				{
					writer.WriteLine("    <Reference Include=\"" + reference.Include + "\" />");
				}
				else
				{
					writer.WriteLine("    <Reference Include=\"" + reference.Include + "\">");
					if(reference.SpecificVersion.Length > 0)
					{
						writer.WriteLine("      <SpecificVersion>" + reference.SpecificVersion + "</SpecificVersion>");
					}
					if(reference.HintPath.Length > 0)
					{
						writer.WriteLine("      <HintPath>" + reference.HintPath + "</HintPath>");
					}
					writer.WriteLine("    </Reference>");
				}
			}
			writer.WriteLine("  </ItemGroup>");

			writer.WriteLine("  <ItemGroup>");
			foreach(Models.Item item in project.Items)
			{
				switch(item.Type)
				{
					case "Compile" :
					{
						if(item.SubType.Length == 0
							&& item.AutoGen.Length == 0 
							&& item.DependentUpon.Length == 0 
							&& item.DesignTimeSharedInput.Length == 0 
							&& item.Generator.Length == 0 
							&& item.LastGenOutput.Length == 0 
							&& item.DesignTime.Length == 0 
							)
						{
							writer.WriteLine("    <Compile Include=\"" + item.Include + "\" />");
						}
						else
						{
							writer.WriteLine("    <Compile Include=\"" + item.Include + "\">");
							if(item.SubType.Length > 0)
							{
								writer.WriteLine("      <SubType>" + item.SubType + "</SubType>");
							}
							if(item.AutoGen.Length > 0)
							{
								writer.WriteLine("      <AutoGen>" + item.AutoGen + "</AutoGen>");
							}
							if(item.DependentUpon.Length > 0)
							{
								writer.WriteLine("      <DependentUpon>" + item.DependentUpon + "</DependentUpon>");
							}
							if(item.DesignTime.Length > 0)
							{
								writer.WriteLine("      <DesignTime>" + item.DesignTime + "</DesignTime>");
							}
							if(item.DesignTimeSharedInput.Length > 0)
							{
								writer.WriteLine("      <DesignTimeSharedInput>" + item.DesignTimeSharedInput + "</DesignTimeSharedInput>");
							}
							if(item.Generator.Length > 0)
							{
								writer.WriteLine("      <Generator>" + item.Generator + "</Generator>");
							}
							if(item.LastGenOutput.Length > 0)
							{
								writer.WriteLine("      <LastGenOutput>" + item.LastGenOutput + "</LastGenOutput>");
							}
							writer.WriteLine("    </Compile>");
						}
						break;
					}
					case "EmbeddedResource" :
					{
						writer.WriteLine("    <EmbeddedResource Include=\"" + item.Include + "\">");
						if(item.DependentUpon.Length > 0)
						{
							writer.WriteLine("      <DependentUpon>" + item.DependentUpon + "</DependentUpon>");
						}
						if(item.Generator.Length > 0)
						{
							writer.WriteLine("      <Generator>" + item.Generator + "</Generator>");
						}
						if(item.LastGenOutput.Length > 0)
						{
							writer.WriteLine("      <LastGenOutput>" + item.LastGenOutput + "</LastGenOutput>");
						}
						if(item.SubType.Length > 0)
						{
							writer.WriteLine("      <SubType>" + item.SubType + "</SubType>");
						}

						writer.WriteLine("    </EmbeddedResource>");
						break;
					}
					case "None" :
					{
						if(item.Generator.Length == 0
							&& item.LastGenOutput.Length == 0 
							&& item.DependentUpon.Length == 0 
							&& item.SubType.Length == 0 
							)
						{
							writer.WriteLine("    <None Include=\"" + item.Include + "\" />");
						}
						else
						{
							writer.WriteLine("    <None Include=\"" + item.Include + "\">");
							if(item.DependentUpon.Length > 0)
							{
								writer.WriteLine("      <DependentUpon>" + item.DependentUpon + "</DependentUpon>");
							}
							if(item.Generator.Length > 0)
							{
								writer.WriteLine("      <Generator>" + item.Generator + "</Generator>");
							}
							if(item.LastGenOutput.Length > 0)
							{
								writer.WriteLine("      <LastGenOutput>" + item.LastGenOutput + "</LastGenOutput>");
							}
							if(item.SubType.Length > 0)
							{
								writer.WriteLine("      <SubType>" + item.SubType + "</SubType>");
							}
							writer.WriteLine("    </None>");
						}
						break;
					}
					case "Content" :
					{
						break;
					}
					default :
					{
						throw new System.Exception("Unknown item type: " + item.Type);
					}
				}
			}
			writer.WriteLine("  </ItemGroup>");

			if(HasContent(project.Items))
			{
				writer.WriteLine("  <ItemGroup>");
				foreach(Models.Item item in project.Items)
				{
					switch(item.Type)
					{
						case "Compile" :
						{
							break;
						}
						case "EmbeddedResource" :
						{
							break;
						}
						case "None" :
						{
							break;
						}
						case "Content" :
						{
							writer.WriteLine("    <Content Include=\"" +  item.Include+ "\" />");
							break;
						}
						default :
						{
							throw new System.Exception("Unknown item type: " + item.Type);
						}
					}
				}
				writer.WriteLine("  </ItemGroup>");
			}

			if(project.Folders.Count > 0)
			{
				writer.WriteLine("  <ItemGroup>");
				foreach(Models.Folder folder in project.Folders)
				{
					writer.WriteLine("    <Folder Include=\"" +  folder.Include+ "\" />");
				}
				writer.WriteLine("  </ItemGroup>");
			}

			if(project.BootstrapperFile != null)
			{
				writer.WriteLine("  <ItemGroup>");
				writer.WriteLine("    <BootstrapperFile Include=\"" + project.BootstrapperFile.Include + "\">");
				writer.WriteLine("      <InProject>" + project.BootstrapperFile.InProject + "</InProject>");
				writer.WriteLine("      <ProductName>" + project.BootstrapperFile.ProductName + "</ProductName>");
				writer.WriteLine("      <Install>" + project.BootstrapperFile.Install + "</Install>");
				writer.WriteLine("    </BootstrapperFile>");
				writer.WriteLine("  </ItemGroup>");
			}

			if(project.BootstrapperPackage != null)
			{
				writer.WriteLine("  <ItemGroup>");
				writer.WriteLine("    <BootstrapperPackage Include=\"" + project.BootstrapperFile.Include + "\">");
				writer.WriteLine("      <InProject>" + project.BootstrapperFile.InProject + "</InProject>");
				writer.WriteLine("      <ProductName>" + project.BootstrapperFile.ProductName + "</ProductName>");
				writer.WriteLine("      <Install>" + project.BootstrapperFile.Install + "</Install>");
				writer.WriteLine("    </BootstrapperPackage>");
				writer.WriteLine("  </ItemGroup>");
			}

			if(project.ProjectReferences.Count > 0)
			{
				writer.WriteLine("  <ItemGroup>");
				foreach(Models.ProjectReference projectReference in project.ProjectReferences)
				{
					writer.WriteLine("    <ProjectReference Include=\"" + projectReference.Include + "\">");
					writer.WriteLine("      <Project>" + projectReference.Project + "</Project>");
					writer.WriteLine("      <Name>" + projectReference.Name + "</Name>");
					writer.WriteLine("    </ProjectReference>");
				}
				writer.WriteLine("  </ItemGroup>");
			}

			if(project.Import != null)
			{
				writer.WriteLine("  <Import Project=\"" + project.Import.Project + "\" />");
			}

			writer.WriteLine("  <!-- To modify your build process, add your task inside one of the targets below and uncomment it. ");
			writer.WriteLine("       Other similar extension points exist, see Microsoft.Common.targets.");
			writer.WriteLine("  <Target Name=\"BeforeBuild\">");
			writer.WriteLine("  </Target>");
			writer.WriteLine("  <Target Name=\"AfterBuild\">");
			writer.WriteLine("  </Target>");
			writer.WriteLine("  -->");

			writer.Write("</Project>");

			writer.Flush();
		}

		private static bool HasContent(List<Models.Item> items)
		{
			foreach(Models.Item item in items)
			{
				switch(item.Type)
				{
					case "Content" :
					{
						return true;
					}
				}
			}
			return false;
		}

		private ProjectWriter()
		{
		}

	}
}

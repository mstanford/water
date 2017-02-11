// ------------------------------------------------------------------------------
// 
// Copyright (c) 2005 Swampware, Inc.
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
	/// Summary description for ProjectWriter.
	/// </summary>
	public class ProjectWriter
	{

		/// <summary>
		/// Writes a project to a file.
		/// </summary>
		/// <param name="solution"></param>
		/// <param name="filename"></param>
		public static void Write(Models.Project project, System.IO.TextWriter writer)
		{
			writer.WriteLine("<VisualStudioProject>");
			writer.WriteLine("    <CSHARP");
			writer.WriteLine("        ProjectType = \"" + project.ProjectType + "\"");
			writer.WriteLine("        ProductVersion = \"" + project.ProductVersion + "\"");
			writer.WriteLine("        SchemaVersion = \"" + project.SchemaVersion + "\"");
			writer.WriteLine("        ProjectGuid = \"{" + project.ProjectGuid.ToString().ToUpper() + "}\"");
			writer.WriteLine("    >");
			writer.WriteLine("        <Build>");
			writer.WriteLine("            <Settings");
			writer.WriteLine("                ApplicationIcon = \"" + project.Settings.ApplicationIcon + "\"");
			writer.WriteLine("                AssemblyKeyContainerName = \"" + project.Settings.AssemblyKeyContainerName + "\"");
			writer.WriteLine("                AssemblyName = \"" + project.Settings.AssemblyName + "\"");
			writer.WriteLine("                AssemblyOriginatorKeyFile = \"" + project.Settings.AssemblyOriginatorKeyFile + "\"");
			writer.WriteLine("                DefaultClientScript = \"" + project.Settings.DefaultClientScript + "\"");
			writer.WriteLine("                DefaultHTMLPageLayout = \"" + project.Settings.DefaultHTMLPageLayout + "\"");
			writer.WriteLine("                DefaultTargetSchema = \"" + project.Settings.DefaultTargetSchema + "\"");
			writer.WriteLine("                DelaySign = \"" + project.Settings.DelaySign + "\"");
			writer.WriteLine("                OutputType = \"" + project.Settings.OutputType + "\"");
			writer.WriteLine("                PreBuildEvent = \"" + project.Settings.PreBuildEvent + "\"");
			writer.WriteLine("                PostBuildEvent = \"" + project.Settings.PostBuildEvent + "\"");
			writer.WriteLine("                RootNamespace = \"" + project.Settings.RootNamespace + "\"");
			writer.WriteLine("                RunPostBuildEvent = \"" + project.Settings.RunPostBuildEvent + "\"");
			writer.WriteLine("                StartupObject = \"" + project.Settings.StartupObject + "\"");
			writer.WriteLine("            >");
			foreach(Models.Config config in project.Configs)
			{
				writer.WriteLine("                <Config");
				writer.WriteLine("                    Name = \"" + config.Name + "\"");
				writer.WriteLine("                    AllowUnsafeBlocks = \"" + config.AllowUnsafeBlocks + "\"");
				writer.WriteLine("                    BaseAddress = \"" + config.BaseAddress + "\"");
				writer.WriteLine("                    CheckForOverflowUnderflow = \"" + config.CheckForOverflowUnderflow + "\"");
				writer.WriteLine("                    ConfigurationOverrideFile = \"" + config.ConfigurationOverrideFile + "\"");
				writer.WriteLine("                    DefineConstants = \"" + config.DefineConstants + "\"");
				writer.WriteLine("                    DocumentationFile = \"" + config.DocumentationFile + "\"");
				writer.WriteLine("                    DebugSymbols = \"" + config.DebugSymbols + "\"");
				writer.WriteLine("                    FileAlignment = \"" + config.FileAlignment + "\"");
				writer.WriteLine("                    IncrementalBuild = \"" + config.IncrementalBuild + "\"");
				writer.WriteLine("                    NoStdLib = \"" + config.NoStdLib + "\"");
				writer.WriteLine("                    NoWarn = \"" + config.NoWarn + "\"");
				writer.WriteLine("                    Optimize = \"" + config.Optimize + "\"");
				writer.WriteLine("                    OutputPath = \"" + config.OutputPath + "\"");
				writer.WriteLine("                    RegisterForComInterop = \"" + config.RegisterForComInterop + "\"");
				writer.WriteLine("                    RemoveIntegerChecks = \"" + config.RemoveIntegerChecks + "\"");
				writer.WriteLine("                    TreatWarningsAsErrors = \"" + config.TreatWarningsAsErrors + "\"");
				writer.WriteLine("                    WarningLevel = \"" + config.WarningLevel + "\"");
				writer.WriteLine("                />");
			}
			writer.WriteLine("            </Settings>");
			writer.WriteLine("            <References>");
			foreach(Models.Reference reference in project.References)
			{
				writer.WriteLine("                <Reference");
				writer.WriteLine("                    Name = \"" + reference.Name + "\"");
				if(reference.AssemblyName.Length > 0)
				{
					writer.WriteLine("                    AssemblyName = \"" + reference.AssemblyName + "\"");
				}
				if(reference.HintPath.Length > 0)
				{
					writer.WriteLine("                    HintPath = \"" + reference.HintPath + "\"");
				}
				if(reference.Project.Length > 0)
				{
					writer.WriteLine("                    Project = \"" + reference.Project + "\"");
				}
				if(reference.Package.Length > 0)
				{
					writer.WriteLine("                    Package = \"" + reference.Package + "\"");
				}
				if(reference.Guid.Length > 0)
				{
					writer.WriteLine("                    Guid = \"" + reference.Guid + "\"");
				}
				if(reference.VersionMajor.Length > 0)
				{
					writer.WriteLine("                    VersionMajor = \"" + reference.VersionMajor + "\"");
				}
				if(reference.VersionMinor.Length > 0)
				{
					writer.WriteLine("                    VersionMinor = \"" + reference.VersionMinor + "\"");
				}
				if(reference.Lcid.Length > 0)
				{
					writer.WriteLine("                    Lcid = \"" + reference.Lcid + "\"");
				}
				if(reference.WrapperTool.Length > 0)
				{
					writer.WriteLine("                    WrapperTool = \"" + reference.WrapperTool + "\"");
				}
				if(reference.Private.Length > 0)
				{
					writer.WriteLine("                    Private = \"" + reference.Private + "\"");
				}
				if(reference.AssemblyFolderKey.Length > 0)
				{
					writer.WriteLine("                    AssemblyFolderKey = \"" + reference.AssemblyFolderKey + "\"");
				}
				writer.WriteLine("                />");
			}
			writer.WriteLine("            </References>");
			writer.WriteLine("        </Build>");
			writer.WriteLine("        <Files>");
			writer.WriteLine("            <Include>");
			foreach(Models.Item file in project.Items)
			{
				writer.WriteLine("                <File");
				if(file.RelPath.Length > 0)
				{
					writer.WriteLine("                    RelPath = \"" + file.RelPath + "\"");
				}
				if(file.DependentUpon.Length > 0)
				{
					writer.WriteLine("                    DependentUpon = \"" + file.DependentUpon + "\"");
				}
				if(file.SubType.Length > 0)
				{
					writer.WriteLine("                    SubType = \"" + file.SubType + "\"");
				}
				if(file.BuildAction.Length > 0)
				{
					writer.WriteLine("                    BuildAction = \"" + file.BuildAction + "\"");
				}
				if(file.Generator.Length > 0)
				{
					writer.WriteLine("                    Generator = \"" + file.Generator + "\"");
				}
				if(file.LastGenOutput.Length > 0)
				{
					writer.WriteLine("                    LastGenOutput = \"" + file.LastGenOutput + "\"");
				}
				if(file.DesignTime.Length > 0)
				{
					writer.WriteLine("                    DesignTime = \"" + file.DesignTime + "\"");
				}
				if(file.AutoGen.Length > 0)
				{
					writer.WriteLine("                    AutoGen = \"" + file.AutoGen + "\"");
				}
				writer.WriteLine("                />");
			}
			foreach(Models.Folder folder in project.Folders)
			{
				writer.WriteLine("                <Folder RelPath = \"" + folder.RelPath + "\" />");
			}
			writer.WriteLine("            </Include>");
			writer.WriteLine("        </Files>");
			writer.WriteLine("    </CSHARP>");
			writer.WriteLine("</VisualStudioProject>");
			writer.WriteLine("");

			writer.Flush();
		}

		private ProjectWriter()
		{
		}

	}
}

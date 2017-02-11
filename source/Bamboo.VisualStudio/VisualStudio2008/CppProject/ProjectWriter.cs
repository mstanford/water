// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace Bamboo.VisualStudio.VisualStudio2008.CppProject
{
	/// <summary>
	/// Summary description for ProjectWriter.
	/// </summary>
	public class ProjectWriter
	{

		public static void Write(Models.Project project, System.IO.TextWriter writer)
		{
			writer.WriteLine("<?xml version=\"1.0\" encoding=\"Windows-1252\"?>");
			writer.WriteLine("<VisualStudioProject");
			writer.WriteLine("	ProjectType=\"Visual C++\"");
			writer.WriteLine("	Version=\"9.00\"");
			writer.WriteLine("	Name=\"" + project.Name + "\"");
			writer.WriteLine("	ProjectGUID=\"{" + project.ProjectGuid.ToString().ToUpper() + "}\"");
			writer.WriteLine("	RootNamespace=\"" + project.RootNamespace + "\"\"");
			writer.WriteLine("	TargetFrameworkVersion=\"196613\"");
			writer.WriteLine("	>");
			writer.WriteLine("	<Platforms>");
			writer.WriteLine("		<Platform");
			writer.WriteLine("			Name=\"Win32\"");
			writer.WriteLine("		/>");
			writer.WriteLine("	</Platforms>");
			writer.WriteLine("	<ToolFiles>");
			writer.WriteLine("	</ToolFiles>");
			writer.WriteLine("	<Configurations>");
			foreach (Models.Configuration configuration in project.Configurations)
			{
				writer.WriteLine("		<Configuration");
				writer.WriteLine("			Name=\"" + configuration.Name + "\"");
				writer.WriteLine("			OutputDirectory=\"" + configuration.OutputDirectory + "\"");
				writer.WriteLine("			IntermediateDirectory=\"" + configuration.IntermediateDirectory + "\"");
				writer.WriteLine("			ConfigurationType=\"4\"");
				writer.WriteLine("			CharacterSet=\"2\"");
				writer.WriteLine("			>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCPreBuildEventTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCCustomBuildTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCXMLDataGeneratorTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCWebServiceProxyGeneratorTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCMIDLTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCCLCompilerTool\"");
				writer.WriteLine("				Optimization=\"0\"");
				writer.WriteLine("				MinimalRebuild=\"true\"");
				writer.WriteLine("				BasicRuntimeChecks=\"3\"");
				writer.WriteLine("				RuntimeLibrary=\"3\"");
				writer.WriteLine("				WarningLevel=\"3\"");
				writer.WriteLine("				DebugInformationFormat=\"4\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCManagedResourceCompilerTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCResourceCompilerTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCPreLinkEventTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCLibrarianTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCALinkTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCXDCMakeTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCBscMakeTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCFxCopTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("			<Tool");
				writer.WriteLine("				Name=\"VCPostBuildEventTool\"");
				writer.WriteLine("			/>");
				writer.WriteLine("		</Configuration>");
			}
			writer.WriteLine("	</Configurations>");
			writer.WriteLine("	<References>");
//			foreach (Models.Reference reference in project.References)
//			{
//				TODO
//			}
			writer.WriteLine("	</References>");
			writer.WriteLine("	<Files>");
			foreach (Models.FileFilter fileFilter in project.FileFilters)
			{
				writer.WriteLine("		<Filter");
				writer.WriteLine("			Name=\"" + fileFilter.Name + "\"");
				writer.WriteLine("			Filter=\"" + fileFilter.Filter + "\"");
				writer.WriteLine("			UniqueIdentifier=\"{" + fileFilter.UniqueIdentifier.ToString().ToUpper() + "}\"");
				writer.WriteLine("			>");
				foreach (Models.File file in fileFilter.Files)
				{
					writer.WriteLine("			<File");
					writer.WriteLine("				RelativePath=\"" + file.RelativePath + "\"");
					writer.WriteLine("				>");
					writer.WriteLine("			</File>");
				}
				writer.WriteLine("		</Filter>");
			}
			writer.WriteLine("	</Files>");
			writer.WriteLine("	<Globals>");
			writer.WriteLine("	</Globals>");
			writer.WriteLine("</VisualStudioProject>");

			writer.Flush();
		}

		private ProjectWriter()
		{
		}

	}
}

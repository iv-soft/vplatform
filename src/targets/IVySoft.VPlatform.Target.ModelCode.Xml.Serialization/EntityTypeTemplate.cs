#pragma checksum "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eaa2c48db03a460469a50a0beaf8f8dc8ed071cd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Razor.EntityTypeTemplate), @"default", @"/EntityTypeTemplate.cs.vtt")]
namespace Razor
{
    #line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eaa2c48db03a460469a50a0beaf8f8dc8ed071cd", @"/EntityTypeTemplate.cs.vtt")]
    public class EntityTypeTemplate : IVySoft.VPlatform.Target.ModelCode.Xml.Serialization.EntityTypeContext
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("using System.Collections.Generic;\r\nusing System.ComponentModel.DataAnnotations;\r\nusing System.ComponentModel.DataAnnotations.Schema;\r\nusing System.Xml.Serialization;\r\nusing System.Linq;\r\n\r\nnamespace ");
#line 8 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
     Write(Module.Namespace);

#line default
#line hidden
#line 8 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                      Write();

#line default
#line hidden
            WriteLiteral(".Xml.Serialization\r\n{\r\n    [XmlRoot(Namespace = XmlConfig.Namespace)]\r\n    public class ");
#line 11 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
            Write(EntityType.Name);

#line default
#line hidden
            WriteLiteral("\r\n    {\r\n");
#line 13 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
         foreach(var property in EntityType.Properties)
		{
			

#line default
#line hidden
#line 15 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             if(property.Multiplicity != "0..*" && property.Multiplicity != "1..*")
			{

#line default
#line hidden
            WriteLiteral("\t\t");
            WriteLiteral("[XmlElement()]\r\n\t\t");
            WriteLiteral("public ");
#line 18 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
            Write(property.Type);

#line default
#line hidden
            WriteLiteral(" ");
#line 18 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                           Write(property.Name);

#line default
#line hidden
            WriteLiteral(" { get; set; }\r\n");
#line 19 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
			}

#line default
#line hidden
#line 19 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             
		}

#line default
#line hidden
            WriteLiteral("\r\n");
#line 22 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
         foreach(var property in EntityType.Properties)
		{
			

#line default
#line hidden
#line 24 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             if(property.Multiplicity == "0..*" || property.Multiplicity == "1..*")
			{

#line default
#line hidden
            WriteLiteral("        ");
            WriteLiteral("[XmlArray()]\r\n\t\t");
            WriteLiteral("public ");
#line 27 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
            Write(property.Type);

#line default
#line hidden
#line 27 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                          Write();

#line default
#line hidden
            WriteLiteral("[] ");
#line 27 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                              Write(property.Name);

#line default
#line hidden
            WriteLiteral(" { get; set; }\r\n");
#line 28 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
			}

#line default
#line hidden
#line 28 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             
		}

#line default
#line hidden
            WriteLiteral("\r\n\t\tpublic ");
#line 31 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
          Write(Module.Namespace);

#line default
#line hidden
            WriteLiteral(".");
#line 31 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                            Write(EntityType.Name);

#line default
#line hidden
            WriteLiteral(" ToModel()\r\n        {\r\n            return new ");
#line 33 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                  Write(Module.Namespace);

#line default
#line hidden
            WriteLiteral(".");
#line 33 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                                    Write(EntityType.Name);

#line default
#line hidden
            WriteLiteral("\r\n            {\r\n");
#line 35 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             foreach(var property in EntityType.Properties)
			{
				

#line default
#line hidden
#line 37 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                 if(property.Multiplicity != "0..*" && property.Multiplicity != "1..*")
				{

#line default
#line hidden
            WriteLiteral("\t\t\t\t\t");
#line 39 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                 Write(property.Name);

#line default
#line hidden
            WriteLiteral(" = this.");
#line 39 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                                       Write(property.Name);

#line default
#line hidden
            WriteLiteral(",\r\n");
#line 40 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
				}

#line default
#line hidden
#line 40 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                 
			}

#line default
#line hidden
            WriteLiteral("\t\t");
#line 42 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
         foreach(var property in EntityType.Properties)
		{
			

#line default
#line hidden
#line 44 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             if(property.Multiplicity == "0..*" || property.Multiplicity == "1..*")
			{

#line default
#line hidden
            WriteLiteral("\t\t");
#line 46 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
     Write(property.Name);

#line default
#line hidden
            WriteLiteral(" = new List<");
#line 46 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                               Write(Module.Namespace);

#line default
#line hidden
            WriteLiteral(".");
#line 46 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                                                 Write(property.Type);

#line default
#line hidden
            WriteLiteral(">(this.");
#line 46 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                                                                      Write(property.Name);

#line default
#line hidden
#line 46 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
                                                                                    Write();

#line default
#line hidden
            WriteLiteral(".Select(x => x.ToModel())),\r\n");
#line 47 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
			}

#line default
#line hidden
#line 47 "C:\Users\User\projects\vplatform\src\targets\IVySoft.VPlatform.Target.ModelCode.Xml.Serialization\EntityTypeTemplate.cs.vtt"
             
		}

#line default
#line hidden
            WriteLiteral("            };\r\n        }\r\n\r\n    }\r\n}");
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591

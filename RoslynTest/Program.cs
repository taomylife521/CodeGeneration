
using CodeGeneration.helper;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace RoslynTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = @"
namespace RoslynTest
{
        public interface ICalculator
        {
           
            public static int Evaluate(string a,string b);
            public static string Evaluate2(string a1,string b1);
        }
}";
            string path = @"E:\ND.Application\ND.Lib.Application\NDLib\ND.WebService\ND.WebService.Contract\NDFront.WebService.Contract\hotel\IHotelFacility.cs";
            text = File.ReadAllText(path);
        SyntaxTree tree=  Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree.ParseText(text);
        var root = (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)tree.GetRoot();
        var firstMember = root.Members[0];
        NamespaceDeclarationSyntax NameSpaceDeclaration = (NamespaceDeclarationSyntax)firstMember;
        InterfaceDeclarationSyntax interfaceDeclaration = (InterfaceDeclarationSyntax)NameSpaceDeclaration.Members[0];
        Console.WriteLine("命名空间:" + NameSpaceDeclaration.Name);
        Console.WriteLine("接口名:" + interfaceDeclaration.Identifier.Value);
        var collctor = new UsingCollector();//收集非System程序集的引用
        collctor.Visit(root);
        foreach (var item in collctor.Usings)
        {
            Console.WriteLine(item.Name.ToString());
        }
           
        IEnumerable<SyntaxAnnotation> ss = interfaceDeclaration.GetAnnotations("ICalculator");
       ss.ToList().ForEach(x =>
       {
           Console.WriteLine("接口描述:" + x.Data); 
       });
      
        interfaceDeclaration.Members.ToList().ForEach(y =>
        {
            
            MethodDeclarationSyntax methodDeclaration = (MethodDeclarationSyntax)y;
            var paramsDeclaration = methodDeclaration.ParameterList.Parameters;
            
            Console.WriteLine("返回类型:" + methodDeclaration.ReturnType + "方法名称:" + methodDeclaration.Identifier);
            paramsDeclaration.ToList().ForEach(x =>
            {
                
                Console.WriteLine("参数类型:" + x.Type + ",参数名称:" + x.Identifier);
            });
        });
            
     
       
          Console.ReadKey();
        }

      
    }
}

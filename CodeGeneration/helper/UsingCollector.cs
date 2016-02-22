using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.helper
{
    /// <summary>
    /// 收集引用
    /// </summary>
   public class UsingCollector:CSharpSyntaxWalker
    {
       public readonly List<UsingDirectiveSyntax> Usings = new List<UsingDirectiveSyntax>();
       public override void VisitUsingDirective(UsingDirectiveSyntax node)
       {
           base.VisitUsingDirective(node);
           if(node.Name.ToString() != "System" && !node.Name.ToString().StartsWith("System."))
           {
               this.Usings.Add(node);
           }
       }
    }
}

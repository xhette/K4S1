using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CSharp;

namespace ChystLabs
{
	public class FunctionParser
	{
        private string code = @"
        public static class __CompiledExpr__
        {{
            public static {0} Run({1})
            {{
                return {2};
            }}
        }}
        ";

        private string expr = "";

        public FunctionParser (string expression)
		{
            expr = expression.Replace("pow", "System.Math.Pow")
                    .Replace("sin", "System.Math.Sin")
                    .Replace("cos", "System.Math.Cos")
                    .Replace("log", "System.Math.Log")
                    .Replace("abs", "System.Math.Abs")
                    .Replace("ctg", "1/System.Math.Tan")
                    .Replace("tg", "System.Math.Tan")
                    .Replace("pi", "System.Math.PI")
                    .Replace("exp", "System.Math.Exp")
                    .Replace("sqrt", "System.Math.Sqrt");
        }

        public MethodInfo ToMethod(Type[] argTypes, string[] argNames, Type resultType)
        {
            StringBuilder argString = new StringBuilder();
            for (int i = 0; i < argTypes.Length; i++)
            {
                if (i != 0) argString.Append(", ");
                argString.AppendFormat("{0} {1}", argTypes[i].FullName, argNames[i]);
            }
            string finalCode = string.Format(code, resultType != null ? resultType.FullName : "void",
                argString, expr);

            var parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("mscorlib.dll");
            parameters.ReferencedAssemblies.Add(Path.GetFileName(Assembly.GetExecutingAssembly().Location));
            parameters.GenerateInMemory = true;

            var c = new CSharpCodeProvider();
            CompilerResults results = c.CompileAssemblyFromSource(parameters, finalCode);
            var asm = results.CompiledAssembly;
            var compiledType = asm.GetType("__CompiledExpr__");
            return compiledType.GetMethod("Run");
        }

        public Func<T1, T2, TResult> ToFunc<T1, T2, TResult>(string arg1Name, string arg2Name)
        {
            var method = ToMethod(new Type[] { typeof(T1), typeof(T2) },
                new string[] { arg1Name, arg2Name }, typeof(TResult));
            return (T1 arg1, T2 arg2) => (TResult)method.Invoke(null, new object[] { arg1, arg2 });
        }
    }
}

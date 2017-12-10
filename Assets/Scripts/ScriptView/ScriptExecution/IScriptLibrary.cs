public interface IScriptLibrary
{
    string FunctionFileContents { get; }
    Script GetScript(string scriptName);
}
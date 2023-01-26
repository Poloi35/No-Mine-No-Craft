

using System.Collections.Generic;

public class ModuleFunction : Module
{
    private List<Module> modules = new List<Module>();

    public void AddModule(Module mod){
        modules.Add(mod);
    }

    public void RemoveModule(Module mod){
        modules.Remove(mod);
    }

    public override void Execute()
    {
        modules[0].Execute();
    }
}
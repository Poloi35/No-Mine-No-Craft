

using System.Collections.Generic;

public class ModuleFunction : Module
{
    private List<Module> modules = new List<Module>();

    public void addModule(Module mod){
        if (modules.Count > 0)
            mod.addInput(modules[modules.Count-1]);
        modules.Add(mod);
    }

    public void removeModule(Module mod){
        modules.Remove(mod);
    }

    public override void execute()
    {
        modules[0].execute();
    }
}
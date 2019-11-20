using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramPlanning.Shared.Services
{
    public interface ISettingsManager<T>
    {
        T LoadSettings();
        void SaveSettings(T settings);
    }
}

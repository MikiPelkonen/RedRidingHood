using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Commands
{
    public interface ICommand
    {
        void Run(Character character);
    }
}

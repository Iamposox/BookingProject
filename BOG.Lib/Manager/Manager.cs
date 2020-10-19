using BOG.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOG.Lib.Manager
{
    public class Manager
    {
        private readonly Context context;
        public Manager() => context = new Context();
    }
}

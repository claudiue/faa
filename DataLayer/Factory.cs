﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Factory
    {
        public IDataAccess CreateDataAccess()
        {
            return new DataAccess(new FileManager());
        }

        public IFileManager CreateFileManager() 
        {
            return new FileManager();
        }
    }
}
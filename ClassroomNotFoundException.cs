﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProjectClassroom;

public class ClassroomNotFoundException:Exception
{
    public ClassroomNotFoundException(string message) : base(message)
    {
        
    }
}
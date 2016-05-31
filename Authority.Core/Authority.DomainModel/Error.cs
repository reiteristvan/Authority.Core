﻿using System;

namespace Authority.Core
{
    public sealed class Error : EntityBase
    {
        public string Type { get; set; }

        public string StackTrace { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}

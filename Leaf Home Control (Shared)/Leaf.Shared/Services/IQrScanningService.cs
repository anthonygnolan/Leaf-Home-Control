﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaf.Shared.Services
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.DTOs.PhotoDTO
{
    public class FileResponse
    {
        public int FileID { get; set; }
        public string Filename { get; set; }
        public string Uri { get; set; }
    }
}

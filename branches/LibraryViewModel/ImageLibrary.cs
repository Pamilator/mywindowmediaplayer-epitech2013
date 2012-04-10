﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MWMP.Models;
using MWMP.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MWMP.Utils;
using MWMP;
using MWMP.Models.DAL;

namespace LibraryViewModel
{
    class ImageLibrary : GenericLibrary<IImageMedia>
    {
        protected override bool CanAdd(IImageMedia media)
        {
            foreach (IImageMedia currentMedia in MediaList)
            {
                if (currentMedia.Path == media.Path)
                    return false;
            }
            return true;
        }

        public override bool FilterOperator(object source)
        {
            throw new NotImplementedException();
        }
    }
}

﻿using Blazui.Component;
using Blazui.Component.Form;
using Blazui.Markdown.IconHandlers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazui.Markdown
{
    public class LinkBase : BComponentBase
    {
        internal protected BForm form;

        [Parameter]
        public LinkModel Link { get; set; }

        internal protected void Submit()
        {
            if (!form.IsValid())
            {
                return;
            }
            _ = DialogService.CloseDialogAsync(this, form.GetValue<LinkModel>());
        }
    }
}

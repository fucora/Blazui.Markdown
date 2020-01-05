﻿using Blazui.Component;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Markdown.IconHandlers
{
    public class LinkHandler : IIconHandler
    {
        private readonly IJSRuntime jSRuntime;
        private readonly DialogService dialogService;

        public LinkHandler(IJSRuntime jSRuntime, DialogService dialogService)
        {
            this.jSRuntime = jSRuntime;
            this.dialogService = dialogService;
        }

        public async Task HandleAsync(ElementReference textarea)
        {
            var linkName = await jSRuntime.InvokeAsync<string>("getSelection", textarea);
            var linkModel = new LinkModel
            {
                Name = linkName,
                Title = linkName
            };
            var parameters = new Dictionary<string, object>();
            parameters.Add(nameof(Link.Link), linkModel);
            var result = await dialogService.ShowDialogAsync<Link, LinkModel>("插入链接", parameters);
            linkModel = result.Result;
            var title = linkModel.Title;
            if (!string.IsNullOrWhiteSpace(title))
            {
                title = $"\"{title}\"";
            }
            var link = $"[{linkModel.Name}]({linkModel.Url} {title})";
            await jSRuntime.InvokeVoidAsync("replaceSelection", textarea, link);
        }
    }
}

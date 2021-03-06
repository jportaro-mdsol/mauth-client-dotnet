﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Medidata.MAuth.Tests.Infrastructure
{
    internal class RequestBodyAsNonSeekableMiddleware
    {
        private readonly RequestDelegate next;

        public RequestBodyAsNonSeekableMiddleware(RequestDelegate next) => this.next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Body != null && context.Request.Body != Stream.Null && context.Request.Body.CanSeek)
            {
                var body = new NonSeekableStream(context.Request.Body);
                context.Request.Body = body;
            }

            await next.Invoke(context);
        }
    }
}

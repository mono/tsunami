//
// RemoveDirectoryContext.cs
//
// Author:
//   Alan McGovern <alan.mcgovern@gmail.com>
//
// Copyright (C) 2008 Alan McGovern.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

namespace Tsunami
{
    class RemoveDirectoryContext : Context
    {
        public RemoveDirectoryContext(Context parent)
        {
            Parent = parent;
        }

        protected override Result HandleImpl(string line)
        {
            if (string.IsNullOrEmpty(line))
                return Result.ShouldPop;

            Option option = Options.GetSelected(line);
            if (option == null)
                return Result.Handled;

            ((GeneralContext)BaseContext).Tracker.RemoveWatcher(option.Description);
            return Result.ShouldPop;
        }

        protected override void PrintImpl(System.IO.TextWriter writer)
        {
            Options.Clear();
            foreach (string s in ((GeneralContext)BaseContext).Tracker.Watchers.Keys)
                Options.Add(new Option(s, (Options.Count + 1).ToString()));
            
            writer.WriteLine("Choose the directory to stop monitoring");
            base.PrintImpl(writer);
        }
    }
}

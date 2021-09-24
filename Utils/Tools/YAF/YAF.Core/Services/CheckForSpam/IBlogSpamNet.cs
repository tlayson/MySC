﻿/* Based on BlogSpam.net API http://blogspamnetapi.codeplex.com/
 * 
 * The MIT License (MIT)
 * -------------------------------------
 * Copyright (c) 2011 Code Gecko
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace YAF.Core.Services.CheckForSpam
{
    using CookComputing.XmlRpc;

    /// <summary>
    /// The BlogSpamNet Interface
    /// </summary>
    public interface IBlogSpamNet
    {
        #region Public Methods

        /// <summary>
        /// The classify comment.
        /// </summary>
        /// <param name="commentToTrain">
        /// The comment to train.
        /// </param>
        /// <returns>
        /// The classify comment.
        /// </returns>
        [XmlRpcMethod("classifyComment")]
        string classifyComment(TrainComment commentToTrain);

        /// <summary>
        /// The get stats.
        /// </summary>
        /// <param name="siteUrl">
        /// The site url.
        /// </param>
        /// <returns>
        /// Returns the Stats
        /// </returns>
        [XmlRpcMethod("getStats")]
        Stats getStats(string siteUrl);

        /// <summary>
        /// The test comment.
        /// </summary>
        /// <param name="comment">
        /// The comment.
        /// </param>
        /// <returns>
        /// The test comment.
        /// </returns>
        [XmlRpcMethod("testComment")]
        string testComment(BlogSpamComment comment);

        #endregion
    }
}
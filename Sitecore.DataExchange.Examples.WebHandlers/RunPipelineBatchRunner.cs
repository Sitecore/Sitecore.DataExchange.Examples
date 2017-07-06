
namespace Sitecore.DataExchange.Examples.WebHandlers
{
    using System;
    using Data;
    using Local.Commands;
    using Shell.Framework.Commands;

    public static class RunPipelineBatchRunner
    {
        public static readonly ID BatchGuid = ID.Parse("{3564BC3C-6AD3-4D8F-83B5-03EB1478793D}");
        
        public static string RunPipelineBatch(string id = null)
        {
            var comm = new RunPipelineBatchCommand();
            ID batchId;

            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    batchId = ID.Parse(id);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                batchId = BatchGuid;
            }

            Data.Items.Item batchItem = Configuration.Factory.GetDatabase("master").GetItem(batchId);

            if (batchItem != null)
            {
                CommandContext context = new CommandContext(batchItem);

                comm.Execute(context);

                return batchItem.DisplayName;
            }
            return null;
        }
    }
}

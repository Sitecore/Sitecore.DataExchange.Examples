You'll need to add to web.config:
 <handlers>
	...
 
		<add verb="*" path="pipelinebatchhandler.ashx" type="Sitecore.DataExchange.Examples.WebHandlers.RunPipelineBatchHandler, Sitecore.DataExchange.Examples.WebHandlers" name="Sitecore_DataExchange_Examples_WebHandlers_runPipelineBatch" />


	...
 </handlers>


than can be runned via HTTP requests:
http://<sitename>/-/pipelinebatchhandler/?id={<Pipeline Batch GUID>}
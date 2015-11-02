using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Wyam.Core;
using Wyam.Core.Configuration;
using Wyam.Core.Modules;
using Wyam.Common.IO;
using Wyam.Common.Pipelines;
using Wyam.Common.Tracing;
using Wyam.Common.Caching;
using Wyam.Common.Configuration;
using Wyam.Common.Modules;
using Wyam.Common.Documents;
using Wyam.Modules.CodeAnalysis;
using Wyam.Modules.Download;
using Wyam.Modules.Git;
using Wyam.Modules.Html;
using Wyam.Modules.Images;
using Wyam.Modules.Less;
using Wyam.Modules.Markdown;
using Wyam.Modules.Razor;
using Wyam.Modules.TextGeneration;
using Wyam.Modules.Yaml;
#line 9

// Declaration code

	public class Tree<T, K> : IComparable<Tree<T, K>> where K : IComparable<K>
    {

        public Tree<T, K> PreviousSilbling
        {
            get
            {
                if (Parent == null)
                    return null; // Root hat keine Geschwister

                var parentIndex = IndexInParent;
				if(parentIndex< 0)
				  throw new ApplicationException("Not found in Parent");
                if (parentIndex == 0)
                    return null;
                return Parent.Childrean[parentIndex - 1];
            }
        }

        public Tree<T, K> NextSilbling
        {
            get
            {
                if (Parent == null)
                    return null; // Root hat keine Geschwister

                var parentIndex = IndexInParent;
				if(parentIndex< 0)
				  throw new ApplicationException("Not found in Parent");
                if (parentIndex == Parent.Childrean.Count - 1)
                    return null;
                return Parent.Childrean[parentIndex + 1];
            }
        }

        public int IndexInParent
        {
            get
            {
                return this.Parent.childrean.BinarySearch(this, new TreeIndexSearcher());
            }
        }


        private int Level => Parent == null ? 0 : Parent.Level + 1;

        private Tree<T, K> parent;
        public Tree<T, K> Parent
        {
            get { return parent; }
            set
            {
                if (parent != null)
                {
                    var index = parent.childrean.BinarySearch(this, new TreeIndexSearcher());
                    if (index < 0)
                        throw new ApplicationException($"The Element should have been in the Parrent Childrean {value}.");
                    parent.childrean.RemoveAt(index);
                }

                parent = value;

                if (parent != null)
                {
                    var index = parent.childrean.BinarySearch(this, new TreeIndexSearcher());
                    if (index < 0)
                        index = ~index;						
                    parent.childrean.Insert(index, this);
                }
            }
        }

        public T Value { get; }
        public K Key { get; }

        public System.Collections.ObjectModel.ReadOnlyCollection<Tree<T, K>> Childrean => childrean.AsReadOnly();
        private readonly List<Tree<T, K>> childrean = new List<Tree<T, K>>();


        public Tree(T value, K key)
        {
            this.Value = value;
            this.Key = key;
        }

        public int CompareTo(Tree<T, K> other)
        {
            var me = this;
			if(other == null)
				throw new ArgumentException("other is null");
            while (me.Level > other.Level)
            {
                if (me.Level - 1 == other.Level && me.Parent == other)
                    return -1;
                me = me.Parent;
            }
            while (other.Level > me.Level)
            {
                if (other.Level - 1 == me.Level && other.Parent == me)
                    return +1;
                other = other.Parent;
            }
            while (me.Parent != null && other.Parent != null)
            {
                if (other.Parent == me.Parent)
                {
                    return me.IndexInParent.CompareTo(other.IndexInParent);
                }
                me = me.Parent;
                other = other.Parent;
            }
            throw new ArgumentException("Both trees not comparable", nameof(other));
        }


        private class TreeIndexSearcher : IComparer<Tree<T, K>>
        {

            public int Compare(Tree<T, K> x, Tree<T, K> y)
            {
				if(x == null)
					throw new ArgumentNullException("Darf net null sein.",nameof(x));
				if(y == null)
					throw new ArgumentNullException("Darf net null sein.",nameof(y));
                return x.Key.CompareTo(y.Key);
            }
        }
    }

public static class	Constants
{
	public static string ProjectName {get; }= "nota";
	
	const string timeZoneZerilisation = "W. Europe Standard Time;60;(UTC+01:00) Amsterdam, Berlin, Bern, Rom, Stockholm, Wien;Mitteleuropäische Zeit;Mitteleuropäische Sommerzeit;[01:01:0001;12:31:9999;60;[0;02:00:00;3;5;0;];[0;03:00:00;10;5;0;];];";
	
	public static TimeZoneInfo TimeZone {get; } = TimeZoneInfo.FromSerializedString(timeZoneZerilisation); 
}

public static class AutoLinkHelper
{
	public static IEnumerable<string> GetKeywords(IDocument doc, params String[] keywords)
	{
		foreach (var keyword in keywords)
		{
			if(doc.Metadata.ContainsKey(keyword))
			{
				var metadata = doc[keyword];
				var strings = metadata as String[];
				if(strings != null)
					foreach (var s in strings)
						yield return s;
				else
					yield return metadata.ToString();
			}
		}
	}
	
	public static ILookup<string,IDocument> ToLookup(IExecutionContext ctx)
	{
		var erg = ctx.Documents
    .FromPipeline("renderOutput")  // Get documents from our current pipeline
    .Select(x =>  AutoLinkHelper.GetKeywords(x,"Title","Alias")
					.Select(y => new {Document = x, Keyword = y}))
	.SelectMany(x=>x)
	.ToLookup(x=> x.Keyword, x=> x.Document);
		return erg;
	}
}


                public static class ConfigScript
                {
                    public static void Run(IDictionary<string, object> Metadata, IPipelineCollection Pipelines, string RootFolder, string InputFolder, string OutputFolder)
                    {
#line 182

// Configuration code

//  88                                              
//  88                                       ,d     
//  88                                       88     
//  88 8b,dPPYba,  8b,dPPYba,  88       88 MM88MMM  
//  88 88P'   `"8a 88P'    "8a 88       88   88     
//  88 88       88 88       d8 88       88   88     
//  88 88       88 88b,   ,a8" "8a,   ,a88   88,    
//  88 88       88 88`YbbdP"'   `"YbbdP'Y8   "Y888  
//                 88                               
//                 88                  



Pipelines.Add("renderInput",
	ConfigScript.ReadFiles(@"render\*"),
	ConfigScript.FrontMatter(ConfigScript.Yaml()),
	ConfigScript.GitContributor(),
	ConfigScript.DirectoryMeta()
		.WithMetadataFile(".inherited",true),
	ConfigScript.Meta("TargetFile",  (@doc, @ctx)=> 
	{
		var path = ((string)@doc["RelativeFilePath"]).Split('\\','/').Skip(1).ToArray();
		var last = path[path.Length-1];
		if(Path.GetExtension(last) == ".md" || Path.GetExtension(last) == ".cshtml")
			path[path.Length-1] = last = Path.ChangeExtension(last, ".html");
		return Path.Combine(path);
	}),
	ConfigScript.Meta("TargetUrl",  (@doc, @ctx)=> 
	{
		var url = (string)@doc["TargetFile"];
		if(Path.GetFileName(url) == "index.html")
			url = Path.GetDirectoryName(url);
		url = "\\"+url;
		return url;
	}),
	ConfigScript.Meta("ReposetoryUrl",  (@doc, @ctx)=> 
	{		
		//https://github.com/LokiMidgard/nota/blob/master/input/render/Downloads.md
		var url = "https://github.com/LokiMidgard/nota/blob/master/input/"+ (string)@doc["RelativeFilePath"];
		return url;
	})
);
 

Pipelines.Add("staticInput",
	ConfigScript.ReadFiles(@"static\*"),
	ConfigScript.Meta("TargetFile",  (@doc, @ctx)=> 
	{
		var path = ((string)@doc["RelativeFilePath"]).Split('\\','/').Skip(1).ToArray();
		var last = path[path.Length-1];
		return Path.Combine(path);
	}),
		ConfigScript.Meta("TargetUrl",  (@doc, @ctx)=> 
	{
		var url = (string)@doc["TargetFile"];
		if(Path.GetFileName(url) == "index.html")
			url = Path.GetDirectoryName(url);
		url = "\\"+url;
		return url;
	})
);

//  888888888888                                           ad88                                           
//       88                                               d8"                                             
//       88                                               88                                              
//       88 8b,dPPYba, ,adPPYYba, 8b,dPPYba,  ,adPPYba, MM88MMM ,adPPYba,  8b,dPPYba, 88,dPYba,,adPYba,   
//       88 88P'   "Y8 ""     `Y8 88P'   `"8a I8[    ""   88   a8"     "8a 88P'   "Y8 88P'   "88"    "8a  
//       88 88         ,adPPPPP88 88       88  `"Y8ba,    88   8b       d8 88         88      88      88  
//       88 88         88,    ,88 88       88 aa    ]8I   88   "8a,   ,a8" 88         88      88      88  
//       88 88         `"8bbdP"Y8 88       88 `"YbbdP"'   88    `"YbbdP"'  88         88      88      88  

 
Pipelines.Add("bootstrapOutput",
	ConfigScript.ReadFiles(@"Scripts\*"),
	ConfigScript.Meta("TargetFile",  (@doc, @ctx)=> 
	{
		var path = ((string)@doc["RelativeFilePath"]).Split('\\','/').ToArray();
		path[0]="js";
		return Path.Combine(path);
	}),
	ConfigScript.Concat(	
		ConfigScript.ReadFiles("bootstrap.less"),
		ConfigScript.Less(),
		ConfigScript.Meta("TargetFile",  @"css\bootstrap.css")
	),
	ConfigScript.Concat(	
		ConfigScript.ReadFiles(@"Content\fonts\*"),
		ConfigScript.Meta("TargetFile",  (@doc, @ctx)=> 
		{
			var path = ((string)@doc["RelativeFilePath"]).Split('\\','/').Skip(1).ToArray();
			return Path.Combine(path);
		})
	)
);


Pipelines.Add("authorOutput",
	ConfigScript.GitAuthors(),
	ConfigScript.Meta("TargetFile",  (@doc,_)=>Path.Combine("author",((Author)@doc["Contributor"]).Email+".html")),
	ConfigScript.Meta("Title", (@doc,_)=>((Author)@doc["Contributor"]).Name),
	ConfigScript.Meta("Layout", @"layout\book.cshtml"),
	ConfigScript.Content(
		ConfigScript.ReadFiles(@"template\author.cshtml")		
	),
	ConfigScript.BranchConcat(
		ConfigScript.Execute((@doc, @ctx)=>{
			var information = @doc["CommitInformation"] as IEnumerable<CommitInformation>;
			var fileCommits =  information.GroupBy(x=>x.Path);

			return fileCommits.Select(x=>
			{
				var targetFile = Path.Combine("author",((Author)@doc["Contributor"]).Email,x.Key+".html");
				var commits = x.ToArray();				
				return @ctx.GetNewDocument(new KeyValuePair<string,object>[]
				{
					new KeyValuePair<string,object>("Contributor", @doc["Contributor"]),
					new KeyValuePair<string,object>("ContributorSite", @doc["TargetFile"]),
					new KeyValuePair<string,object>("TargetFile", targetFile),
					new KeyValuePair<string,object>("Commits", commits),
					new KeyValuePair<string,object>("Path", x.Key),
				});				
			});
		}),
		ConfigScript.Content(
			ConfigScript.ReadFiles(@"template\CommitFile.cshtml")		
		)
	),
	ConfigScript.Meta("TargetUrl",  (@doc, @ctx)=> 
	{
		var url = (string)@doc["TargetFile"];
		if(Path.GetFileName(url) == "index.html")
			url = Path.GetDirectoryName(url);
		url = "\\"+url;
		return url;
	}),
	ConfigScript.Execute((@doc,_)=>new IDocument[]{ @doc.Clone(Path.ChangeExtension((string)@doc["TargetFile"], ".cshtml"),@doc.Content)})
);

Pipelines.Add("renderOutput",
	ConfigScript.Documents("renderInput"),
	ConfigScript.Concat(
		ConfigScript.Execute((@ctx)=>
		{
			var ambigiusElements = AutoLinkHelper.ToLookup(@ctx).Where(x=> x.Skip(1).Any());
			
			var list = new List<IDocument>();
			foreach (var a in ambigiusElements)
			{

				list.Add(@ctx.GetNewDocument(
					@"ambiguis\"+a.Key + ".cshtml",
					"",
					new KeyValuePair<string,object>[]{
					new KeyValuePair<string,object>("DisambigiusData", a),
					new KeyValuePair<string,object>("Keyword", a.Key),
					new KeyValuePair<string,object>("Alias", a.Key),
					new KeyValuePair<string,object>("Documents", a),
					new KeyValuePair<string,object>("IsDisambigiusPage", true),
				}));
			}
			return list;
		}),
		ConfigScript.Meta("TargetFile",  (@doc,_)=>Path.Combine("ambiguis",@doc["Keyword"]+".html")),
		ConfigScript.Meta("Title",  (@doc,_)=>"begriffsklärung "+ @doc["Keyword"]),
		ConfigScript.Meta("TargetUrl",  (@doc, @ctx)=> 
		{
			var url = (string)@doc["TargetFile"];
			if(Path.GetFileName(url) == "index.html")
				url = Path.GetDirectoryName(url);
			url = "\\"+url;
			return url;
		}),
		ConfigScript.ReadFiles(@"template\ambiguis.cshtml")
	),
	/// Adding Tree ///
	ConfigScript.Execute(@ctx => {
	 	var input = @ctx.Documents.FromPipeline("renderOutput");
		
		var treeLookup = input.ToDictionary(x=>x["TargetUrl"], x=> new Tree<string,int>((string)x["TargetUrl"], x.Get<int>("ChapterOrder",-1)));
		
		return input.Select(x=>
		{
			var url = (string)x["TargetUrl"];
			var tree = treeLookup[url];
			string parentUrl = url;
			do{
			   parentUrl = Path.GetDirectoryName(parentUrl);
				
			} while(parentUrl != null &&!treeLookup.ContainsKey(parentUrl) );
			if(parentUrl!=null)
			{
				var parentTree = treeLookup[parentUrl];
				tree.Parent = parentTree;
			}
			return x.Clone(new KeyValuePair<string,object>[]{new KeyValuePair<string,object>("Tree", tree)});
			
		});
	}),
	/// BOOKS /// 
	ConfigScript.If((@doc, @ctx)=> 
		{
			var tree = (Tree<string,int>)@doc["Tree"];
			var parentUrl = tree.Parent?.Value;
			if(parentUrl == @"\books")
			{
				tree.Parent = null;
				return true;
			}
			return false;
		},
		ConfigScript.Meta("IsBook", true)
	),
	/// Chapters ///
	ConfigScript.If( (@doc, @ctx)=> 
		{
			var tree = (Tree<string,int>)@doc["Tree"];
			var parent = tree.Parent;
			var parentUrl = parent?.Value;
			if(parentUrl == @"\books" || parentUrl == null)
				return false;
			while(parentUrl != @"\books" && parentUrl != null)
			{
				 parent = parent.Parent;
				 parentUrl = parent?.Value;	
			}
			return parentUrl != null;
		},
		ConfigScript.Meta("ParentBook", (@doc, @ctx)=>{
			var tree = (Tree<string,int>)@doc["Tree"];
			string parentUrl;

			do {
				parentUrl = tree.Parent?.Value;
				tree = tree.Parent;
			} while(parentUrl != @"\books");
			return tree.Value;
		}),
		ConfigScript.If((@doc,_)=>!@doc.ContainsKey("ChapterOrder"),
			ConfigScript.Trace((@doc,_)=>$"Chapter besitzt kein ChapterOrder Metadata: {@doc.Source}"),
			ConfigScript.Where((@doc, @ctx)=> false)
		)
			
	)
);

Pipelines.Add("staticOutput",
	ConfigScript.Documents("staticInput")	
);

Pipelines.Add("output",
	ConfigScript.Documents("staticOutput"),
	ConfigScript.Concat(
		ConfigScript.Documents("renderOutput")
	),
	ConfigScript.Concat(
		ConfigScript.Documents("bootstrapOutput")
	),
	ConfigScript.Concat(
		ConfigScript.Documents("authorOutput")		
	)
);

//  88                                 88                                          
//  88                                 88                                          
//  88                                 88                                          
//  88          ,adPPYba,   ,adPPYba,  88   ,d8 88       88 8b,dPPYba,  ,adPPYba,  
//  88         a8"     "8a a8"     "8a 88 ,a8"  88       88 88P'    "8a I8[    ""  
//  88         8b       d8 8b       d8 8888[    88       88 88       d8  `"Y8ba,   
//  88         "8a,   ,a8" "8a,   ,a8" 88`"Yba, "8a,   ,a88 88b,   ,a8" aa    ]8I  
//  88888888888 `"YbbdP"'   `"YbbdP"'  88   `Y8a `"YbbdP'Y8 88`YbbdP"'  `"YbbdP"'  
//                                                          88                     
//                                                          88                     



Pipelines.Add("Books",
	ConfigScript.Documents("renderOutput").Where((@doc,_)=>@doc.Get<bool>("IsBook")  )	
);




Pipelines.Add("MainMenue",
	ConfigScript.Documents("output").Where( (@doc, @ctx)=> 
	{
		return @doc.Metadata.ContainsKey("MainMenueOrder");
	}),
	ConfigScript.OrderBy((@doc,_)=>@doc["MainMenueOrder"])	
);

//  I8,        8        ,8I          88                    
//  `8b       d8b       d8'          ""   ,d               
//   "8,     ,8"8,     ,8"                88               
//    Y8     8P Y8     8P 8b,dPPYba, 88 MM88MMM ,adPPYba,  
//    `8b   d8' `8b   d8' 88P'   "Y8 88   88   a8P_____88  
//     `8a a8'   `8a a8'  88         88   88   8PP"""""""  
//      `8a8'     `8a8'   88         88   88,  "8b,   ,aa  
//       `8'       `8'    88         88   "Y888 `"Ybbd8"'  
 

Pipelines.Add("Write",
	ConfigScript.Documents("output"),
	ConfigScript.If((@doc,_)=>Path.GetExtension(@doc.Source)==".md",
		ConfigScript.Markdown()
	), 
	ConfigScript.If((@doc,_)=>Path.GetExtension(@doc.Source)==".md" || Path.GetExtension(@doc.Source)==".cshtml",
		ConfigScript.Razor().WithViewStart(@"layout\viewstart.cshtml")
	),
	ConfigScript.AutoLink(@ctx=>(IDictionary<string,string>)AutoLinkHelper.ToLookup(@ctx)  // Use our new ToLookup extension
		.ToDictionary(x => x.Key, 
			x => (string)x.OrderBy(y=>y.ContainsKey("IsDisambigiusPage") ? 0 : 1).First()["TargetUrl"])  )
		.WithMatchOnlyWholeWord(),

	
	ConfigScript.If((@doc,_)=>Path.GetExtension((string)@doc["TargetFile"])==".html",
		ConfigScript.HtmlEscape().WithEscapedChar('ä','ö','ü','Ä','Ö','Ü','ß')
	),

	ConfigScript.WriteFiles( (@doc,_)=>@doc["TargetFile"])
);






                    }
                        public static Wyam.Core.Modules.DirectoryMeta DirectoryMeta()
                        {
                            return new Wyam.Core.Modules.DirectoryMeta();  
                        }
                        public static Wyam.Core.Modules.FileName FileName()
                        {
                            return new Wyam.Core.Modules.FileName();  
                        }
                        public static Wyam.Core.Modules.FileName FileName(string inputKey)
                        {
                            return new Wyam.Core.Modules.FileName(inputKey);  
                        }
                        public static Wyam.Core.Modules.FileName FileName(Wyam.Common.Configuration.DocumentConfig fileName)
                        {
                            return new Wyam.Core.Modules.FileName(fileName);  
                        }
                        public static Wyam.Core.Modules.FileName FileName(string inputKey, string outputKey)
                        {
                            return new Wyam.Core.Modules.FileName(inputKey, outputKey);  
                        }
                        public static Wyam.Core.Modules.FileName FileName(Wyam.Common.Configuration.DocumentConfig fileName, string outputKey)
                        {
                            return new Wyam.Core.Modules.FileName(fileName, outputKey);  
                        }
                        public static Wyam.Core.Modules.ForEach ForEach(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.ForEach(modules);  
                        }
                        public static Wyam.Core.Modules.BranchConcat BranchConcat(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.BranchConcat(modules);  
                        }
                        public static Wyam.Core.Modules.GroupBy GroupBy(Wyam.Common.Configuration.DocumentConfig key, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.GroupBy(key, modules);  
                        }
                        public static Wyam.Core.Modules.UnwrittenFiles UnwrittenFiles(Wyam.Common.Configuration.DocumentConfig path)
                        {
                            return new Wyam.Core.Modules.UnwrittenFiles(path);  
                        }
                        public static Wyam.Core.Modules.UnwrittenFiles UnwrittenFiles(string extension)
                        {
                            return new Wyam.Core.Modules.UnwrittenFiles(extension);  
                        }
                        public static Wyam.Core.Modules.UnwrittenFiles UnwrittenFiles()
                        {
                            return new Wyam.Core.Modules.UnwrittenFiles();  
                        }
                        public static Wyam.Core.Modules.Index Index()
                        {
                            return new Wyam.Core.Modules.Index();  
                        }
                        public static Wyam.Core.Modules.OrderBy OrderBy(Wyam.Common.Configuration.DocumentConfig key)
                        {
                            return new Wyam.Core.Modules.OrderBy(key);  
                        }
                        public static Wyam.Core.Modules.Paginate Paginate(int pageSize, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Paginate(pageSize, modules);  
                        }
                        public static Wyam.Core.Modules.CopyFiles CopyFiles(Wyam.Common.Configuration.DocumentConfig sourcePath)
                        {
                            return new Wyam.Core.Modules.CopyFiles(sourcePath);  
                        }
                        public static Wyam.Core.Modules.CopyFiles CopyFiles(string searchPattern)
                        {
                            return new Wyam.Core.Modules.CopyFiles(searchPattern);  
                        }
                        public static Wyam.Core.Modules.Documents Documents()
                        {
                            return new Wyam.Core.Modules.Documents();  
                        }
                        public static Wyam.Core.Modules.Documents Documents(string pipeline)
                        {
                            return new Wyam.Core.Modules.Documents(pipeline);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(Wyam.Common.Configuration.ContextConfig documents)
                        {
                            return new Wyam.Core.Modules.Documents(documents);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(Wyam.Common.Configuration.DocumentConfig documents)
                        {
                            return new Wyam.Core.Modules.Documents(documents);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(int count)
                        {
                            return new Wyam.Core.Modules.Documents(count);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(params string[] content)
                        {
                            return new Wyam.Core.Modules.Documents(content);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(params System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, object>>[] metadata)
                        {
                            return new Wyam.Core.Modules.Documents(metadata);  
                        }
                        public static Wyam.Core.Modules.Documents Documents(params System.Tuple<string, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, object>>>[] contentAndMetadata)
                        {
                            return new Wyam.Core.Modules.Documents(contentAndMetadata);  
                        }
                        public static Wyam.Core.Modules.FrontMatter FrontMatter(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.FrontMatter(modules);  
                        }
                        public static Wyam.Core.Modules.FrontMatter FrontMatter(string delimiter, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.FrontMatter(delimiter, modules);  
                        }
                        public static Wyam.Core.Modules.FrontMatter FrontMatter(char delimiter, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.FrontMatter(delimiter, modules);  
                        }
                        public static Wyam.Core.Modules.Replace Replace(string search, object content)
                        {
                            return new Wyam.Core.Modules.Replace(search, content);  
                        }
                        public static Wyam.Core.Modules.Replace Replace(string search, Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.Replace(search, content);  
                        }
                        public static Wyam.Core.Modules.Replace Replace(string search, Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.Replace(search, content);  
                        }
                        public static Wyam.Core.Modules.Replace Replace(string search, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Replace(search, modules);  
                        }
                        public static Wyam.Core.Modules.ReplaceIn ReplaceIn(string search, object content)
                        {
                            return new Wyam.Core.Modules.ReplaceIn(search, content);  
                        }
                        public static Wyam.Core.Modules.ReplaceIn ReplaceIn(string search, Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.ReplaceIn(search, content);  
                        }
                        public static Wyam.Core.Modules.ReplaceIn ReplaceIn(string search, Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.ReplaceIn(search, content);  
                        }
                        public static Wyam.Core.Modules.ReplaceIn ReplaceIn(string search, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.ReplaceIn(search, modules);  
                        }
                        public static Wyam.Core.Modules.Trace Trace(object content)
                        {
                            return new Wyam.Core.Modules.Trace(content);  
                        }
                        public static Wyam.Core.Modules.Trace Trace(Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.Trace(content);  
                        }
                        public static Wyam.Core.Modules.Trace Trace(Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.Trace(content);  
                        }
                        public static Wyam.Core.Modules.Trace Trace(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Trace(modules);  
                        }
                        public static Wyam.Core.Modules.Concat Concat(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Concat(modules);  
                        }
                        public static Wyam.Core.Modules.Where Where(Wyam.Common.Configuration.DocumentConfig predicate)
                        {
                            return new Wyam.Core.Modules.Where(predicate);  
                        }
                        public static Wyam.Core.Modules.Append Append(object content)
                        {
                            return new Wyam.Core.Modules.Append(content);  
                        }
                        public static Wyam.Core.Modules.Append Append(Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.Append(content);  
                        }
                        public static Wyam.Core.Modules.Append Append(Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.Append(content);  
                        }
                        public static Wyam.Core.Modules.Append Append(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Append(modules);  
                        }
                        public static Wyam.Core.Modules.Branch Branch(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Branch(modules);  
                        }
                        public static Wyam.Core.Modules.Content Content(object content)
                        {
                            return new Wyam.Core.Modules.Content(content);  
                        }
                        public static Wyam.Core.Modules.Content Content(Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.Content(content);  
                        }
                        public static Wyam.Core.Modules.Content Content(Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.Content(content);  
                        }
                        public static Wyam.Core.Modules.Content Content(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Content(modules);  
                        }
                        public static Wyam.Core.Modules.Execute Execute(Wyam.Common.Configuration.DocumentConfig execute)
                        {
                            return new Wyam.Core.Modules.Execute(execute);  
                        }
                        public static Wyam.Core.Modules.Execute Execute(Wyam.Common.Configuration.ContextConfig execute)
                        {
                            return new Wyam.Core.Modules.Execute(execute);  
                        }
                        public static Wyam.Core.Modules.If If(Wyam.Common.Configuration.DocumentConfig predicate, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.If(predicate, modules);  
                        }
                        public static Wyam.Core.Modules.Meta Meta(string key, object metadata)
                        {
                            return new Wyam.Core.Modules.Meta(key, metadata);  
                        }
                        public static Wyam.Core.Modules.Meta Meta(string key, Wyam.Common.Configuration.ContextConfig metadata)
                        {
                            return new Wyam.Core.Modules.Meta(key, metadata);  
                        }
                        public static Wyam.Core.Modules.Meta Meta(string key, Wyam.Common.Configuration.DocumentConfig metadata)
                        {
                            return new Wyam.Core.Modules.Meta(key, metadata);  
                        }
                        public static Wyam.Core.Modules.Meta Meta(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Meta(modules);  
                        }
                        public static Wyam.Core.Modules.Prepend Prepend(object content)
                        {
                            return new Wyam.Core.Modules.Prepend(content);  
                        }
                        public static Wyam.Core.Modules.Prepend Prepend(Wyam.Common.Configuration.ContextConfig content)
                        {
                            return new Wyam.Core.Modules.Prepend(content);  
                        }
                        public static Wyam.Core.Modules.Prepend Prepend(Wyam.Common.Configuration.DocumentConfig content)
                        {
                            return new Wyam.Core.Modules.Prepend(content);  
                        }
                        public static Wyam.Core.Modules.Prepend Prepend(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Core.Modules.Prepend(modules);  
                        }
                        public static Wyam.Core.Modules.ReadFiles ReadFiles(Wyam.Common.Configuration.DocumentConfig path)
                        {
                            return new Wyam.Core.Modules.ReadFiles(path);  
                        }
                        public static Wyam.Core.Modules.ReadFiles ReadFiles(string searchPattern)
                        {
                            return new Wyam.Core.Modules.ReadFiles(searchPattern);  
                        }
                        public static Wyam.Core.Modules.WriteFiles WriteFiles(Wyam.Common.Configuration.DocumentConfig path)
                        {
                            return new Wyam.Core.Modules.WriteFiles(path);  
                        }
                        public static Wyam.Core.Modules.WriteFiles WriteFiles(string extension)
                        {
                            return new Wyam.Core.Modules.WriteFiles(extension);  
                        }
                        public static Wyam.Core.Modules.WriteFiles WriteFiles()
                        {
                            return new Wyam.Core.Modules.WriteFiles();  
                        }
                        public static Wyam.Modules.CodeAnalysis.AnalyzeCSharp AnalyzeCSharp()
                        {
                            return new Wyam.Modules.CodeAnalysis.AnalyzeCSharp();  
                        }
                        public static Wyam.Modules.CodeAnalysis.ReadProject ReadProject(string path)
                        {
                            return new Wyam.Modules.CodeAnalysis.ReadProject(path);  
                        }
                        public static Wyam.Modules.CodeAnalysis.ReadProject ReadProject(Wyam.Common.Configuration.DocumentConfig path)
                        {
                            return new Wyam.Modules.CodeAnalysis.ReadProject(path);  
                        }
                        public static Wyam.Modules.CodeAnalysis.ReadSolution ReadSolution(string path)
                        {
                            return new Wyam.Modules.CodeAnalysis.ReadSolution(path);  
                        }
                        public static Wyam.Modules.CodeAnalysis.ReadSolution ReadSolution(Wyam.Common.Configuration.DocumentConfig path)
                        {
                            return new Wyam.Modules.CodeAnalysis.ReadSolution(path);  
                        }
                        public static Wyam.Modules.Download.Download Download()
                        {
                            return new Wyam.Modules.Download.Download();  
                        }
                        public static Wyam.Modules.Git.GitCommits GitCommits()
                        {
                            return new Wyam.Modules.Git.GitCommits();  
                        }
                        public static Wyam.Modules.Git.GitFileCommits GitFileCommits(string metadataName)
                        {
                            return new Wyam.Modules.Git.GitFileCommits(metadataName);  
                        }
                        public static Wyam.Modules.Git.GitFileCommits GitFileCommits()
                        {
                            return new Wyam.Modules.Git.GitFileCommits();  
                        }
                        public static Wyam.Modules.Git.GitAuthors GitAuthors()
                        {
                            return new Wyam.Modules.Git.GitAuthors();  
                        }
                        public static Wyam.Modules.Git.GitContributor GitContributor(string metadataName)
                        {
                            return new Wyam.Modules.Git.GitContributor(metadataName);  
                        }
                        public static Wyam.Modules.Git.GitContributor GitContributor()
                        {
                            return new Wyam.Modules.Git.GitContributor();  
                        }
                        public static Wyam.Modules.Html.AutoLink AutoLink()
                        {
                            return new Wyam.Modules.Html.AutoLink();  
                        }
                        public static Wyam.Modules.Html.AutoLink AutoLink(System.Collections.Generic.IDictionary<string, string> links)
                        {
                            return new Wyam.Modules.Html.AutoLink(links);  
                        }
                        public static Wyam.Modules.Html.AutoLink AutoLink(Wyam.Common.Configuration.ContextConfig links)
                        {
                            return new Wyam.Modules.Html.AutoLink(links);  
                        }
                        public static Wyam.Modules.Html.AutoLink AutoLink(Wyam.Common.Configuration.DocumentConfig links)
                        {
                            return new Wyam.Modules.Html.AutoLink(links);  
                        }
                        public static Wyam.Modules.Html.Excerpt Excerpt()
                        {
                            return new Wyam.Modules.Html.Excerpt();  
                        }
                        public static Wyam.Modules.Html.Excerpt Excerpt(string querySelector)
                        {
                            return new Wyam.Modules.Html.Excerpt(querySelector);  
                        }
                        public static Wyam.Modules.Html.HtmlEscape HtmlEscape()
                        {
                            return new Wyam.Modules.Html.HtmlEscape();  
                        }
                        public static Wyam.Modules.Html.HtmlQuery HtmlQuery(string querySelector)
                        {
                            return new Wyam.Modules.Html.HtmlQuery(querySelector);  
                        }
                        public static Wyam.Modules.Images.Image Image()
                        {
                            return new Wyam.Modules.Images.Image();  
                        }
                        public static Wyam.Modules.Less.Less Less()
                        {
                            return new Wyam.Modules.Less.Less();  
                        }
                        public static Wyam.Modules.Markdown.Markdown Markdown()
                        {
                            return new Wyam.Modules.Markdown.Markdown();  
                        }
                        public static Wyam.Modules.Razor.Razor Razor(System.Type basePageType = null)
                        {
                            return new Wyam.Modules.Razor.Razor(basePageType);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateContent GenerateContent(object template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateContent(template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateContent GenerateContent(Wyam.Common.Configuration.ContextConfig template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateContent(template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateContent GenerateContent(Wyam.Common.Configuration.DocumentConfig template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateContent(template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateContent GenerateContent(params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateContent(modules);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateMeta GenerateMeta(string key, object template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateMeta(key, template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateMeta GenerateMeta(string key, Wyam.Common.Configuration.ContextConfig template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateMeta(key, template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateMeta GenerateMeta(string key, Wyam.Common.Configuration.DocumentConfig template)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateMeta(key, template);  
                        }
                        public static Wyam.Modules.TextGeneration.GenerateMeta GenerateMeta(string key, params Wyam.Common.Modules.IModule[] modules)
                        {
                            return new Wyam.Modules.TextGeneration.GenerateMeta(key, modules);  
                        }
                        public static Wyam.Modules.Yaml.Yaml Yaml()
                        {
                            return new Wyam.Modules.Yaml.Yaml();  
                        }
                        public static Wyam.Modules.Yaml.Yaml Yaml(string key, bool flatten = false)
                        {
                            return new Wyam.Modules.Yaml.Yaml(key, flatten);  
                        }}
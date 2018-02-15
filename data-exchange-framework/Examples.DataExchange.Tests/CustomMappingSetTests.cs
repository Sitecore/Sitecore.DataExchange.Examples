using Sitecore.DataExchange.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Examples.DataExchange.Tests
{
    public class CustomMappingSetTests
    {
        [Fact]
        public void MappingFailsWithNullMappingContext()
        {
            var mappingSet = new CustomMappingSet();
            Assert.False(mappingSet.Run(null));
        }
        [Fact]
        public void MappingFailsWithNullSourceObject()
        {
            var mappingSet = new CustomMappingSet();
            var context = new MappingContext { Target = "" };
            Assert.False(mappingSet.Run(context));
        }
        [Fact]
        public void MappingFailsWithNullTargetObject()
        {
            var mappingSet = new CustomMappingSet();
            var context = new MappingContext { Source = "" };
            Assert.False(mappingSet.Run(context));
        }
        public class SourceValid { public string Name { get; set; } }
        public class TargetValid { public string Description { get; set; } public DateTime LastUpdated { get; set; } }
        public class SourceInvalid1 { public string Title { get; set; } }
        public class TargetInvalid1 { public string Description { get; set; } }
        public class TargetInvalid2 { public string Description { get; set; } public string LastUpdated { get; set; } }
        [Fact]
        public void MappingRunsWhenSourceObjectHasNoPropertyName()
        {
            var source = new SourceInvalid1();
            var target = new TargetValid();
            var context = new MappingContext { Source = source, Target = target };
            var mappingSet = new CustomMappingSet();
            Assert.True(mappingSet.Run(context));
            Assert.Equal(1, context.RunFail.Count);
            Assert.Equal("Mapping1", context.RunFail.First().Identifier);
            Assert.Equal(1, context.RunSuccess.Count);
            Assert.Equal("Mapping2", context.RunSuccess.First().Identifier);
            Assert.Equal(DateTime.Now.Date, target.LastUpdated);
        }
        [Fact]
        public void MappingRunsWhenTargetObjectHasNoPropertyDescription()
        {
            var source = new SourceValid { Name = "aaa" };
            var target = new TargetInvalid1();
            var context = new MappingContext { Source = source, Target = target };
            var mappingSet = new CustomMappingSet();
            Assert.True(mappingSet.Run(context));
            Assert.Equal(1, context.RunSuccess.Count);
            Assert.Equal("Mapping1", context.RunSuccess.First().Identifier);
            Assert.Equal(source.Name, target.Description);
            Assert.Equal(1, context.RunFail.Count);
            Assert.Equal("Mapping2", context.RunFail.First().Identifier);
        }
        [Fact]
        public void MappingRunsWhenTargetObjectHasPropertyWithWrongType()
        {
            var source = new SourceValid { Name = "aaa" };
            var target = new TargetInvalid2();
            var context = new MappingContext { Source = source, Target = target };
            var mappingSet = new CustomMappingSet();
            Assert.True(mappingSet.Run(context));
            Assert.Equal(1, context.RunSuccess.Count);
            Assert.Equal("Mapping1", context.RunSuccess.First().Identifier);
            Assert.Equal(source.Name, target.Description);
            Assert.Equal(1, context.RunFail.Count);
            Assert.Equal("Mapping2", context.RunFail.First().Identifier);
            Assert.Null(target.LastUpdated);
        }
        [Fact]
        public void MappingRunsProperly()
        {
            var source = new SourceValid { Name = "aaa" };
            var target = new TargetValid();
            var context = new MappingContext { Source = source, Target = target };
            var mappingSet = new CustomMappingSet();
            Assert.True(mappingSet.Run(context));
            Assert.Equal(2, context.RunSuccess.Count);
            Assert.Contains("Mapping1", context.RunSuccess.Select(m => m.Identifier));
            Assert.Contains("Mapping2", context.RunSuccess.Select(m => m.Identifier));
            Assert.Equal(source.Name, target.Description);
            Assert.Equal(DateTime.Now.Date, target.LastUpdated);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace HasValueVsNull
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();
        }
    }

    [ClrJob, MonoJob, CoreJob] // 可以針對不同的 CLR 進行測試
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        public TestRunner()
        {
        }

        [Benchmark]
        public void HasValue() => _test.HasValue();

        [Benchmark]
        public void EqualNull() => _test.EqualNull();
    }

    public class TestClass
    {
        private readonly Guid? _target = null;

        public void HasValue()
        {
            var result = _target.HasValue;
        }

        public void EqualNull()
        {
            var result = _target == null;
        }
    }
}

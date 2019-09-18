using System;

namespace IVySoft.VPlatform.Generator.Core
{
    public interface IGenerator
    {
        void Generate(IDbModel db, GeneratorOptions options);
    }
}

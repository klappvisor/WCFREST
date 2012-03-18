using System;
using System.Collections.Generic;
namespace DAL
{
    public interface IStorage {
        Symptom CreateNode(string name, Symptom parent);
        void Delete(int nodeId);
        IEnumerable<Symptom> GetChildren(int id);
        Diagnosis GetDiagnosis(int nodeId);
        Symptom GetNode(int id);
        IEnumerable<Symptom> GetRootNodes();
        void SetDiagnosis(int nodeId, Diagnosis diagnosis);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DAL
{
    public class Repository {
        private static List<Symptom> _tree = new List<Symptom> {
            new Symptom { ID = 1, Name = "Node 1" },
            new Symptom { ID = 2, Name = "Node 2" },
            new Symptom { ID = 3, Name = "Node 3" },
            new Symptom { ID = 4, Name = "Node 4" }
        };

        public Repository() {
            CreateNode("Child 1", _tree[1]);
            CreateNode("Child 2", _tree[1]);
            CreateNode("Child 3", _tree[1]);

            CreateNode("Child 1", _tree[2]);
            CreateNode("Child 2", _tree[2]);
        }

        public Symptom CreateNode(string name, Symptom parent) {
            var node = new Symptom { ID = _tree.Count + 1, Name = name, Parent = parent };
            _tree.Add(node);
            return node;
        }

        public IEnumerable<Symptom> GetRootNodes() {
            return _tree.Where(x => x.Parent == null);
        }

        public IEnumerable<Symptom> GetChildren(int id) {
            return _tree.Where(x => x.Parent != null && x.Parent.ID == id);
        }

        public Symptom GetNode(int id) {
            return _tree.FirstOrDefault(x => x.ID == id);
        }

        public void Delete(int nodeId) {
            _tree.ForEach(x => {
                if (x.Parent.ID == nodeId) {
                    x.Parent = null;
                }
            });

            _tree.RemoveAll(x => x.ID == nodeId);
        }

        public Diagnosis GetDiagnosis(int nodeId) {
            var symptom = _tree.FirstOrDefault(x => x.ID == nodeId);
            if(symptom == null)
                return null;
            return symptom.Diagnosis;
        }

        public void SetDiagnosis(int nodeId, Diagnosis diagnosis) {
            var symptom = _tree.FirstOrDefault(x => x.ID == nodeId);
            if (symptom != null) {
                symptom.Diagnosis = diagnosis;
            }
        }
    }

    [DataContract]
    public class Symptom {
        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public String Name { get; set; }
        [IgnoreDataMember]
        public Symptom Parent { get; set; }
        [IgnoreDataMember]
        public Diagnosis Diagnosis {get; set;}

    }

    [DataContract]
    public class Diagnosis {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Recomendation { get; set; }

    }
}


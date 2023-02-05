using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace Common
{
    public interface IDeepCopy
    {
        object CreateDeepCopy();
    }
    
    public interface IEnumerableCollectionPair<T>
    {
        IEnumerableCollection<INode<T>> Nodes { get; }

        IEnumerableCollection<T> Values { get; }
    }
    
    public interface IEnumerableCollection<T> : IEnumerable<T>, ICollection, IEnumerable
    {
        bool Contains(T item);
    }
    
    public interface INode<T> : IEnumerableCollectionPair<T>, IDisposable
  {
    T Data { get; set; }

    string ToStringRecursive();

    int Depth { get; }

    int BranchIndex { get; }

    int BranchCount { get; }

    int Count { get; }

    int DirectChildCount { get; }

    INode<T> Parent { get; }

    INode<T> Previous { get; }

    INode<T> Next { get; }

    INode<T> Child { get; }

    ITree<T> Tree { get; }

    INode<T> Root { get; }

    INode<T> Top { get; }

    INode<T> First { get; }

    INode<T> Last { get; }

    INode<T> LastChild { get; }

    bool IsTree { get; }

    bool IsRoot { get; }

    bool IsTop { get; }

    bool HasParent { get; }

    bool HasPrevious { get; }

    bool HasNext { get; }

    bool HasChild { get; }

    bool IsFirst { get; }

    bool IsLast { get; }

    INode<T> this[T item] { get; }

    bool Contains(INode<T> item);

    bool Contains(T item);

    INode<T> InsertPrevious(T o);

    INode<T> InsertNext(T o);

    INode<T> InsertChild(T o);

    INode<T> Add(T o);

    INode<T> AddChild(T o);

    void InsertPrevious(ITree<T> tree);

    void InsertNext(ITree<T> tree);

    void InsertChild(ITree<T> tree);

    void Add(ITree<T> tree);

    void AddChild(ITree<T> tree);

    ITree<T> Cut(T o);

    ITree<T> Copy(T o);

    ITree<T> DeepCopy(T o);

    bool Remove(T o);

    ITree<T> Cut();

    ITree<T> Copy();

    ITree<T> DeepCopy();

    void Remove();

    bool CanMoveToParent { get; }

    bool CanMoveToPrevious { get; }

    bool CanMoveToNext { get; }

    bool CanMoveToChild { get; }

    bool CanMoveToFirst { get; }

    bool CanMoveToLast { get; }

    void MoveToParent();

    void MoveToPrevious();

    void MoveToNext();

    void MoveToChild();

    void MoveToFirst();

    void MoveToLast();

    IEnumerableCollectionPair<T> All { get; }

    IEnumerableCollectionPair<T> AllChildren { get; }

    IEnumerableCollectionPair<T> DirectChildren { get; }

    IEnumerableCollectionPair<T> DirectChildrenInReverse { get; }

    event EventHandler<NodeTreeDataEventArgs<T>> Validate;

    event EventHandler<NodeTreeDataEventArgs<T>> Setting;

    event EventHandler<NodeTreeDataEventArgs<T>> SetDone;

    event EventHandler<NodeTreeInsertEventArgs<T>> Inserting;

    event EventHandler<NodeTreeInsertEventArgs<T>> Inserted;

    event EventHandler Cutting;

    event EventHandler CutDone;

    event EventHandler<NodeTreeNodeEventArgs<T>> Copying;

    event EventHandler<NodeTreeNodeEventArgs<T>> Copied;

    event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying;

    event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied;
  }
    
    public interface ITree<T> : IEnumerableCollectionPair<T>, IDisposable
    {
        Type DataType { get; }

        IEqualityComparer<T> DataComparer { get; set; }

        void XmlSerialize(Stream stream);

        void Clear();

        int Count { get; }

        int DirectChildCount { get; }

        INode<T> Root { get; }

        INode<T> this[T o] { get; }

        string ToStringRecursive();

        bool Contains(T item);

        bool Contains(INode<T> item);

        INode<T> InsertChild(T o);

        INode<T> AddChild(T o);

        void InsertChild(ITree<T> tree);

        void AddChild(ITree<T> tree);

        ITree<T> Cut(T o);

        ITree<T> Copy(T o);

        ITree<T> DeepCopy(T o);

        bool Remove(T o);

        ITree<T> Copy();

        ITree<T> DeepCopy();

        IEnumerableCollectionPair<T> All { get; }

        IEnumerableCollectionPair<T> AllChildren { get; }

        IEnumerableCollectionPair<T> DirectChildren { get; }

        IEnumerableCollectionPair<T> DirectChildrenInReverse { get; }

        event EventHandler<NodeTreeDataEventArgs<T>> Validate;

        event EventHandler Clearing;

        event EventHandler Cleared;

        event EventHandler<NodeTreeDataEventArgs<T>> Setting;

        event EventHandler<NodeTreeDataEventArgs<T>> SetDone;

        event EventHandler<NodeTreeInsertEventArgs<T>> Inserting;

        event EventHandler<NodeTreeInsertEventArgs<T>> Inserted;

        event EventHandler Cutting;

        event EventHandler CutDone;

        event EventHandler<NodeTreeNodeEventArgs<T>> Copying;

        event EventHandler<NodeTreeNodeEventArgs<T>> Copied;

        event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying;

        event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied;
    }
    
    public class NodeTreeDataEventArgs<T> : EventArgs
    {
        private T _Data = default (T);

        public T Data => this._Data;

        public NodeTreeDataEventArgs(T data) => this._Data = data;
    }
    
    public class NodeTreeInsertEventArgs<T> : EventArgs
    {
        private NodeTreeInsertOperation _Operation;
        private INode<T> _Node;

        public NodeTreeInsertOperation Operation => this._Operation;

        public INode<T> Node => this._Node;

        public NodeTreeInsertEventArgs(NodeTreeInsertOperation operation, INode<T> node)
        {
            this._Operation = operation;
            this._Node = node;
        }
    }
    
    public enum NodeTreeInsertOperation
    {
        Previous,
        Next,
        Child,
        Tree,
    }
    
    public class NodeTreeNodeEventArgs<T> : EventArgs
    {
        private INode<T> _Node;

        public INode<T> Node => this._Node;

        public NodeTreeNodeEventArgs(INode<T> node) => this._Node = node;
    }
    
    [Serializable]
  public class NodeTree<T> : 
    INode<T>,
    ITree<T>,
    IEnumerableCollectionPair<T>,
    IDisposable,
    ISerializable
  {
    private T _Data = default (T);
    private NodeTree<T> _Parent;
    private NodeTree<T> _Previous;
    private NodeTree<T> _Next;
    private NodeTree<T> _Child;
    protected static readonly object XmlAdapterTag = new object();
    private EventHandlerList _EventHandlerList;
    private static readonly object _ValidateEventKey = new object();
    private static readonly object _ClearingEventKey = new object();
    private static readonly object _ClearedEventKey = new object();
    private static readonly object _SettingEventKey = new object();
    private static readonly object _SetDoneEventKey = new object();
    private static readonly object _InsertingEventKey = new object();
    private static readonly object _InsertedEventKey = new object();
    private static readonly object _CuttingEventKey = new object();
    private static readonly object _CutDoneEventKey = new object();
    private static readonly object _CopyingEventKey = new object();
    private static readonly object _CopiedEventKey = new object();
    private static readonly object _DeepCopyingEventKey = new object();
    private static readonly object _DeepCopiedEventKey = new object();

    protected NodeTree()
    {
    }

    ~NodeTree() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this._EventHandlerList == null)
        return;
      this._EventHandlerList.Dispose();
    }

    public static ITree<T> NewTree() => (ITree<T>) new NodeTree<T>.RootObject();

    public static ITree<T> NewTree(IEqualityComparer<T> dataComparer) => (ITree<T>) new NodeTree<T>.RootObject(dataComparer);

    protected static INode<T> NewNode() => (INode<T>) new NodeTree<T>();

    protected virtual NodeTree<T> CreateTree() => (NodeTree<T>) new NodeTree<T>.RootObject();

    protected virtual NodeTree<T> CreateNode() => new NodeTree<T>();

    public override string ToString()
    {
      T data = this.Data;
      return (object) data == null ? string.Empty : data.ToString();
    }

    public virtual string ToStringRecursive()
    {
      string stringRecursive = string.Empty;
      foreach (NodeTree<T> node in (IEnumerable<INode<T>>) this.All.Nodes)
        stringRecursive = stringRecursive + new string('\t', node.Depth) + (object) node + Environment.NewLine;
      return stringRecursive;
    }

    public virtual int Depth
    {
      get
      {
        int depth = -1;
        for (INode<T> node = (INode<T>) this; !node.IsRoot; node = node.Parent)
          ++depth;
        return depth;
      }
    }

    public virtual int BranchIndex
    {
      get
      {
        int branchIndex = -1;
        for (INode<T> node = (INode<T>) this; node != null; node = node.Previous)
          ++branchIndex;
        return branchIndex;
      }
    }

    public virtual int BranchCount
    {
      get
      {
        int branchCount = 0;
        for (INode<T> node = this.First; node != null; node = node.Next)
          ++branchCount;
        return branchCount;
      }
    }

    protected virtual T DeepCopyData(T data)
    {
      if ((object) data == null)
        return default (T);
      switch (data)
      {
        case IDeepCopy deepCopy:
          return (T) deepCopy.CreateDeepCopy();
        case ICloneable cloneable:
          return (T) cloneable.Clone();
        default:
          ConstructorInfo constructor = data.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[1]
          {
            typeof (T)
          }, (ParameterModifier[]) null);
          if (constructor == null)
            return data;
          return (T) constructor.Invoke(new object[1]
          {
            (object) data
          });
      }
    }

    protected virtual NodeTree<T>.RootObject GetRootObject => (NodeTree<T>.RootObject) this.Root;

    public virtual IEqualityComparer<T> DataComparer
    {
      get
      {
        if (!this.Root.IsTree)
          throw new InvalidOperationException("This is not a Tree");
        return this.GetRootObject.DataComparer;
      }
      set
      {
        if (!this.Root.IsTree)
          throw new InvalidOperationException("This is not a Tree");
        this.GetRootObject.DataComparer = value;
      }
    }

    protected virtual int Version
    {
      get
      {
        INode<T> root = this.Root;
        return root.IsTree ? NodeTree<T>.GetNodeTree(root).Version : throw new InvalidOperationException("This is not a Tree");
      }
      set
      {
        INode<T> root = this.Root;
        if (!root.IsTree)
          throw new InvalidOperationException("This is not a Tree");
        NodeTree<T>.GetNodeTree(root).Version = value;
      }
    }

    protected bool HasChanged(int version) => this.Version != version;

    protected void IncrementVersion()
    {
      INode<T> root = this.Root;
      if (!root.IsTree)
        throw new InvalidOperationException("This is not a Tree");
      ++NodeTree<T>.GetNodeTree(root).Version;
    }

    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("NodeTreeVersion", 1);
      info.AddValue("Data", (object) this._Data);
      info.AddValue("Parent", (object) this._Parent);
      info.AddValue("Previous", (object) this._Previous);
      info.AddValue("Next", (object) this._Next);
      info.AddValue("Child", (object) this._Child);
    }

    protected NodeTree(SerializationInfo info, StreamingContext context)
    {
      this._Data = info.GetInt32("NodeTreeVersion") == 1 ? (T) info.GetValue(nameof (Data), typeof (T)) : throw new SerializationException("Unknown version");
      this._Parent = (NodeTree<T>) info.GetValue(nameof (Parent), typeof (NodeTree<T>));
      this._Previous = (NodeTree<T>) info.GetValue(nameof (Previous), typeof (NodeTree<T>));
      this._Next = (NodeTree<T>) info.GetValue(nameof (Next), typeof (NodeTree<T>));
      this._Child = (NodeTree<T>) info.GetValue(nameof (Child), typeof (NodeTree<T>));
    }

    public virtual void XmlSerialize(Stream stream)
    {
      XmlSerializer xmlSerializer;
      try
      {
        xmlSerializer = new XmlSerializer(typeof (NodeTree<T>.TreeXmlSerializationAdapter));
      }
      catch (Exception ex)
      {
        throw;
      }
      try
      {
        xmlSerializer.Serialize(stream, (object) new NodeTree<T>.TreeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, (ITree<T>) this));
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public static ITree<T> XmlDeserialize(Stream stream)
    {
      XmlSerializer xmlSerializer;
      try
      {
        xmlSerializer = new XmlSerializer(typeof (NodeTree<T>.TreeXmlSerializationAdapter));
      }
      catch (Exception ex)
      {
        throw;
      }
      object obj;
      try
      {
        obj = xmlSerializer.Deserialize(stream);
      }
      catch (Exception ex)
      {
        throw;
      }
      return ((NodeTree<T>.TreeXmlSerializationAdapter) obj).Tree;
    }

    public T Data
    {
      get => this._Data;
      set
      {
        if (this.IsRoot)
          throw new InvalidOperationException("This is a Root");
        this.OnSetting((INode<T>) this, value);
        this._Data = value;
        this.OnSetDone((INode<T>) this, value);
      }
    }

    public INode<T> Parent => (INode<T>) this._Parent;

    public INode<T> Previous => (INode<T>) this._Previous;

    public INode<T> Next => (INode<T>) this._Next;

    public INode<T> Child => (INode<T>) this._Child;

    public ITree<T> Tree => (ITree<T>) this.Root;

    public INode<T> Root
    {
      get
      {
        INode<T> root = (INode<T>) this;
        while (root.Parent != null)
          root = root.Parent;
        return root;
      }
    }

    public INode<T> Top
    {
      get
      {
        if (!this.Root.IsTree)
          throw new InvalidOperationException("This is not a tree");
        if (this.IsRoot)
          return (INode<T>) null;
        INode<T> top = (INode<T>) this;
        while (top.Parent.Parent != null)
          top = top.Parent;
        return top;
      }
    }

    public INode<T> First
    {
      get
      {
        INode<T> first = (INode<T>) this;
        while (first.Previous != null)
          first = first.Previous;
        return first;
      }
    }

    public INode<T> Last
    {
      get
      {
        INode<T> last = (INode<T>) this;
        while (last.Next != null)
          last = last.Next;
        return last;
      }
    }

    public INode<T> LastChild => this.Child == null ? (INode<T>) null : this.Child.Last;

    public bool HasPrevious => this.Previous != null;

    public bool HasNext => this.Next != null;

    public bool HasChild => this.Child != null;

    public bool IsFirst => this.Previous == null;

    public bool IsLast => this.Next == null;

    public bool IsTree => this.IsRoot && this is NodeTree<T>.RootObject;

    public bool IsRoot
    {
      get
      {
        bool isRoot = this.Parent == null;
        int num = isRoot ? 1 : 0;
        return isRoot;
      }
    }

    public bool HasParent => !this.IsRoot && this.Parent.Parent != null;

    public bool IsTop => !this.IsRoot && this.Parent.Parent == null;

    public virtual INode<T> this[T item]
    {
      get
      {
        if (!this.Root.IsTree)
          throw new InvalidOperationException("This is not a tree");
        IEqualityComparer<T> dataComparer = this.DataComparer;
        foreach (INode<T> node in (IEnumerable<INode<T>>) this.All.Nodes)
        {
          if (dataComparer.Equals(node.Data, item))
            return node;
        }
        return (INode<T>) null;
      }
    }

    public virtual bool Contains(INode<T> item)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.All.Nodes.Contains(item);
    }

    public virtual bool Contains(T item)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.All.Values.Contains(item);
    }

    public INode<T> InsertPrevious(T o)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> node = this.CreateNode();
      node._Data = o;
      this.InsertPreviousCore((INode<T>) node);
      return (INode<T>) node;
    }

    public INode<T> InsertNext(T o)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> node = this.CreateNode();
      node._Data = o;
      this.InsertNextCore((INode<T>) node);
      return (INode<T>) node;
    }

    public INode<T> InsertChild(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> node = this.CreateNode();
      node._Data = o;
      this.InsertChildCore((INode<T>) node);
      return (INode<T>) node;
    }

    public INode<T> Add(T o)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.Last.InsertNext(o);
    }

    public INode<T> AddChild(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.Child == null ? this.InsertChild(o) : this.Child.Add(o);
    }

    public void InsertPrevious(ITree<T> tree)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
      if (!nodeTree.IsRoot)
        throw new ArgumentException("Tree is not a Root");
      if (!nodeTree.IsTree)
        throw new ArgumentException("Tree is not a tree");
      for (INode<T> node = nodeTree.Child; node != null; node = node.Next)
        this.InsertPreviousCore((INode<T>) NodeTree<T>.GetNodeTree(node).CopyCore());
    }

    public void InsertNext(ITree<T> tree)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
      if (!nodeTree.IsRoot)
        throw new ArgumentException("Tree is not a Root");
      if (!nodeTree.IsTree)
        throw new ArgumentException("Tree is not a tree");
      for (INode<T> node = nodeTree.LastChild; node != null; node = node.Previous)
        this.InsertNextCore((INode<T>) NodeTree<T>.GetNodeTree(node).CopyCore());
    }

    public void InsertChild(ITree<T> tree)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
      if (!nodeTree.IsRoot)
        throw new ArgumentException("Tree is not a Root");
      if (!nodeTree.IsTree)
        throw new ArgumentException("Tree is not a tree");
      for (INode<T> node = nodeTree.LastChild; node != null; node = node.Previous)
        this.InsertChildCore((INode<T>) NodeTree<T>.GetNodeTree(node).CopyCore());
    }

    public void Add(ITree<T> tree)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      this.Last.InsertNext(tree);
    }

    public void AddChild(ITree<T> tree)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      if (this.Child == null)
        this.InsertChild(tree);
      else
        this.Child.Add(tree);
    }

    protected virtual void InsertPreviousCore(INode<T> newINode)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!newINode.IsRoot)
        throw new ArgumentException("Node is not a Root");
      if (newINode.IsTree)
        throw new ArgumentException("Node is a tree");
      this.IncrementVersion();
      this.OnInserting((INode<T>) this, NodeTreeInsertOperation.Previous, newINode);
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
      nodeTree._Parent = this._Parent;
      nodeTree._Previous = this._Previous;
      nodeTree._Next = this;
      this._Previous = nodeTree;
      if (nodeTree.Previous != null)
        NodeTree<T>.GetNodeTree(nodeTree.Previous)._Next = nodeTree;
      else
        NodeTree<T>.GetNodeTree(nodeTree.Parent)._Child = nodeTree;
      this.OnInserted((INode<T>) this, NodeTreeInsertOperation.Previous, newINode);
    }

    protected virtual void InsertNextCore(INode<T> newINode)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!newINode.IsRoot)
        throw new ArgumentException("Node is not a Root");
      if (newINode.IsTree)
        throw new ArgumentException("Node is a tree");
      this.IncrementVersion();
      this.OnInserting((INode<T>) this, NodeTreeInsertOperation.Next, newINode);
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
      nodeTree._Parent = this._Parent;
      nodeTree._Previous = this;
      nodeTree._Next = this._Next;
      this._Next = nodeTree;
      if (nodeTree.Next != null)
        NodeTree<T>.GetNodeTree(nodeTree.Next)._Previous = nodeTree;
      this.OnInserted((INode<T>) this, NodeTreeInsertOperation.Next, newINode);
    }

    protected virtual void InsertChildCore(INode<T> newINode)
    {
      if (!newINode.IsRoot)
        throw new ArgumentException("Node is not a Root");
      if (newINode.IsTree)
        throw new ArgumentException("Node is a tree");
      this.IncrementVersion();
      this.OnInserting((INode<T>) this, NodeTreeInsertOperation.Child, newINode);
      NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
      nodeTree._Parent = this;
      nodeTree._Next = this._Child;
      this._Child = nodeTree;
      if (nodeTree.Next != null)
        NodeTree<T>.GetNodeTree(nodeTree.Next)._Previous = nodeTree;
      this.OnInserted((INode<T>) this, NodeTreeInsertOperation.Child, newINode);
    }

    protected virtual void AddCore(INode<T> newINode)
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      NodeTree<T>.GetNodeTree(this.Last).InsertNextCore(newINode);
    }

    protected virtual void AddChildCore(INode<T> newINode)
    {
      if (this.Child == null)
        this.InsertChildCore(newINode);
      else
        NodeTree<T>.GetNodeTree(this.Child).AddCore(newINode);
    }

    public ITree<T> Cut(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this[o]?.Cut();
    }

    public ITree<T> Copy(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this[o]?.Copy();
    }

    public ITree<T> DeepCopy(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this[o]?.DeepCopy();
    }

    public bool Remove(T o)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      INode<T> node = this[o];
      if (node == null)
        return false;
      node.Remove();
      return true;
    }

    private NodeTree<T> BoxInTree(NodeTree<T> node)
    {
      if (!node.IsRoot)
        throw new ArgumentException("Node is not a Root");
      if (node.IsTree)
        throw new ArgumentException("Node is a tree");
      NodeTree<T> tree = this.CreateTree();
      tree.AddChildCore((INode<T>) node);
      return tree;
    }

    public ITree<T> Cut()
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return (ITree<T>) this.BoxInTree(this.CutCore());
    }

    public ITree<T> Copy()
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.IsTree ? (ITree<T>) this.CopyCore() : (ITree<T>) this.BoxInTree(this.CopyCore());
    }

    public ITree<T> DeepCopy()
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      return this.IsTree ? (ITree<T>) this.DeepCopyCore() : (ITree<T>) this.BoxInTree(this.DeepCopyCore());
    }

    public void Remove()
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      this.RemoveCore();
    }

    protected virtual NodeTree<T> CutCore()
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      this.IncrementVersion();
      this.OnCutting((INode<T>) this);
      INode<T> root = this.Root;
      if (this._Next != null)
        this._Next._Previous = this._Previous;
      if (this.Previous != null)
        this._Previous._Next = this._Next;
      else
        this._Parent._Child = this._Next;
      this._Parent = (NodeTree<T>) null;
      this._Previous = (NodeTree<T>) null;
      this._Next = (NodeTree<T>) null;
      this.OnCutDone(root, (INode<T>) this);
      return this;
    }

    protected virtual NodeTree<T> CopyCore()
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      if (this.IsRoot && !this.IsTree)
        throw new InvalidOperationException("This is a Root");
      if (this.IsTree)
      {
        NodeTree<T> tree = this.CreateTree();
        this.OnCopying((INode<T>) this, (INode<T>) tree);
        this.CopyChildNodes((INode<T>) this, tree, false);
        this.OnCopied((INode<T>) this, (INode<T>) tree);
        return tree;
      }
      NodeTree<T> node = this.CreateNode();
      node._Data = this.Data;
      this.OnCopying((INode<T>) this, (INode<T>) node);
      this.CopyChildNodes((INode<T>) this, node, false);
      this.OnCopied((INode<T>) this, (INode<T>) node);
      return node;
    }

    protected virtual NodeTree<T> DeepCopyCore()
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      if (this.IsRoot && !this.IsTree)
        throw new InvalidOperationException("This is a Root");
      if (this.IsTree)
      {
        NodeTree<T> tree = this.CreateTree();
        this.OnCopying((INode<T>) this, (INode<T>) tree);
        this.CopyChildNodes((INode<T>) this, tree, true);
        this.OnCopied((INode<T>) this, (INode<T>) tree);
        return tree;
      }
      NodeTree<T> node = this.CreateNode();
      node._Data = this.DeepCopyData(this.Data);
      this.OnDeepCopying((INode<T>) this, (INode<T>) node);
      this.CopyChildNodes((INode<T>) this, node, true);
      this.OnDeepCopied((INode<T>) this, (INode<T>) node);
      return node;
    }

    private void CopyChildNodes(INode<T> oldNode, NodeTree<T> newNode, bool bDeepCopy)
    {
      NodeTree<T> nodeTree = (NodeTree<T>) null;
      for (INode<T> oldNode1 = oldNode.Child; oldNode1 != null; oldNode1 = oldNode1.Next)
      {
        NodeTree<T> node = this.CreateNode();
        node._Data = bDeepCopy ? this.DeepCopyData(oldNode1.Data) : oldNode1.Data;
        if (oldNode1.Previous == null)
          newNode._Child = node;
        node._Parent = newNode;
        node._Previous = nodeTree;
        if (nodeTree != null)
          nodeTree._Next = node;
        this.CopyChildNodes(oldNode1, node, bDeepCopy);
        nodeTree = node;
      }
    }

    protected virtual void RemoveCore()
    {
      if (this.IsRoot)
        throw new InvalidOperationException("This is a Root");
      this.CutCore();
    }

    public bool CanMoveToParent => !this.IsRoot && !this.IsTop;

    public bool CanMoveToPrevious => !this.IsRoot && !this.IsFirst;

    public bool CanMoveToNext => !this.IsRoot && !this.IsLast;

    public bool CanMoveToChild => !this.IsRoot && !this.IsFirst;

    public bool CanMoveToFirst => !this.IsRoot && !this.IsFirst;

    public bool CanMoveToLast => !this.IsRoot && !this.IsLast;

    public void MoveToParent()
    {
      if (!this.CanMoveToParent)
        throw new InvalidOperationException("Cannot move to Parent");
      NodeTree<T>.GetNodeTree(this.Parent).InsertNextCore((INode<T>) this.CutCore());
    }

    public void MoveToPrevious()
    {
      if (!this.CanMoveToPrevious)
        throw new InvalidOperationException("Cannot move to Previous");
      NodeTree<T>.GetNodeTree(this.Previous).InsertPreviousCore((INode<T>) this.CutCore());
    }

    public void MoveToNext()
    {
      if (!this.CanMoveToNext)
        throw new InvalidOperationException("Cannot move to Next");
      NodeTree<T>.GetNodeTree(this.Next).InsertNextCore((INode<T>) this.CutCore());
    }

    public void MoveToChild()
    {
      if (!this.CanMoveToChild)
        throw new InvalidOperationException("Cannot move to Child");
      NodeTree<T>.GetNodeTree(this.Previous).AddChildCore((INode<T>) this.CutCore());
    }

    public void MoveToFirst()
    {
      if (!this.CanMoveToFirst)
        throw new InvalidOperationException("Cannot move to first");
      NodeTree<T>.GetNodeTree(this.First).InsertPreviousCore((INode<T>) this.CutCore());
    }

    public void MoveToLast()
    {
      if (!this.CanMoveToLast)
        throw new InvalidOperationException("Cannot move to last");
      NodeTree<T>.GetNodeTree(this.Last).InsertNextCore((INode<T>) this.CutCore());
    }

    public virtual IEnumerableCollection<INode<T>> Nodes => this.All.Nodes;

    public virtual IEnumerableCollection<T> Values => this.All.Values;

    public IEnumerableCollectionPair<T> All => (IEnumerableCollectionPair<T>) new NodeTree<T>.AllEnumerator(this);

    public IEnumerableCollectionPair<T> AllChildren => (IEnumerableCollectionPair<T>) new NodeTree<T>.AllChildrenEnumerator(this);

    public IEnumerableCollectionPair<T> DirectChildren => (IEnumerableCollectionPair<T>) new NodeTree<T>.DirectChildrenEnumerator(this);

    public IEnumerableCollectionPair<T> DirectChildrenInReverse => (IEnumerableCollectionPair<T>) new NodeTree<T>.DirectChildrenInReverseEnumerator(this);

    public int DirectChildCount
    {
      get
      {
        int directChildCount = 0;
        for (INode<T> node = this.Child; node != null; node = node.Next)
          ++directChildCount;
        return directChildCount;
      }
    }

    public virtual Type DataType => typeof (T);

    public void Clear()
    {
      if (!this.IsRoot)
        throw new InvalidOperationException("This is not a Root");
      if (!this.IsTree)
        throw new InvalidOperationException("This is not a tree");
      this.OnClearing((ITree<T>) this);
      this._Child = (NodeTree<T>) null;
      this.OnCleared((ITree<T>) this);
    }

    protected static NodeTree<T> GetNodeTree(ITree<T> tree) => tree != null ? (NodeTree<T>) tree : throw new ArgumentNullException("Tree is null");

    protected static NodeTree<T> GetNodeTree(INode<T> node) => node != null ? (NodeTree<T>) node : throw new ArgumentNullException("Node is null");

    public virtual int Count
    {
      get
      {
        int count = this.IsRoot ? 0 : 1;
        for (INode<T> node = this.Child; node != null; node = node.Next)
          count += node.Count;
        return count;
      }
    }

    public virtual bool IsReadOnly => false;

    protected EventHandlerList EventHandlerList => this._EventHandlerList;

    protected EventHandlerList GetCreateEventHandlerList()
    {
      if (this._EventHandlerList == null)
        this._EventHandlerList = new EventHandlerList();
      return this._EventHandlerList;
    }

    protected static object ValidateEventKey => NodeTree<T>._ValidateEventKey;

    protected static object ClearingEventKey => NodeTree<T>._ClearingEventKey;

    protected static object ClearedEventKey => NodeTree<T>._ClearedEventKey;

    protected static object SettingEventKey => NodeTree<T>._SettingEventKey;

    protected static object SetDoneEventKey => NodeTree<T>._SetDoneEventKey;

    protected static object InsertingEventKey => NodeTree<T>._InsertingEventKey;

    protected static object InsertedEventKey => NodeTree<T>._InsertedEventKey;

    protected static object CuttingEventKey => NodeTree<T>._CuttingEventKey;

    protected static object CutDoneEventKey => NodeTree<T>._CutDoneEventKey;

    protected static object CopyingEventKey => NodeTree<T>._CopyingEventKey;

    protected static object CopiedEventKey => NodeTree<T>._CopiedEventKey;

    protected static object DeepCopyingEventKey => NodeTree<T>._DeepCopyingEventKey;

    protected static object DeepCopiedEventKey => NodeTree<T>._DeepCopiedEventKey;

    public event EventHandler<NodeTreeDataEventArgs<T>> Validate
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ValidateEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ValidateEventKey, (Delegate) value);
    }

    public event EventHandler Clearing
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ClearingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ClearingEventKey, (Delegate) value);
    }

    public event EventHandler Cleared
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ClearedEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ClearedEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeDataEventArgs<T>> Setting
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.SettingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.SettingEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeDataEventArgs<T>> SetDone
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.SetDoneEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.SetDoneEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeInsertEventArgs<T>> Inserting
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.InsertingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.InsertingEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeInsertEventArgs<T>> Inserted
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.InsertedEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.InsertedEventKey, (Delegate) value);
    }

    public event EventHandler Cutting
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CuttingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CuttingEventKey, (Delegate) value);
    }

    public event EventHandler CutDone
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CutDoneEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CutDoneEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeNodeEventArgs<T>> Copying
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CopyingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CopyingEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeNodeEventArgs<T>> Copied
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CopiedEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CopiedEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.DeepCopyingEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.DeepCopyingEventKey, (Delegate) value);
    }

    public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied
    {
      add => this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.DeepCopiedEventKey, (Delegate) value);
      remove => this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.DeepCopiedEventKey, (Delegate) value);
    }

    protected virtual void OnValidate(INode<T> node, T data)
    {
      if (!this.Root.IsTree)
        throw new InvalidOperationException("This is not a tree");
      if ((object) data is INode<T>)
        throw new ArgumentException("Object is a node");
      if ((!typeof (T).IsClass || (object) data != null) && !this.DataType.IsInstanceOfType((object) data))
        throw new ArgumentException("Object is not a " + this.DataType.Name);
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>) this._EventHandlerList[NodeTree<T>.ValidateEventKey];
        if (eventHandler != null)
          eventHandler((object) node, new NodeTreeDataEventArgs<T>(data));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnValidate(node, data);
    }

    protected virtual void OnClearing(ITree<T> tree)
    {
      if (this._EventHandlerList == null)
        return;
      EventHandler eventHandler = (EventHandler) this._EventHandlerList[NodeTree<T>.ClearingEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) tree, EventArgs.Empty);
    }

    protected virtual void OnCleared(ITree<T> tree)
    {
      if (this._EventHandlerList == null)
        return;
      EventHandler eventHandler = (EventHandler) this._EventHandlerList[NodeTree<T>.ClearedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) tree, EventArgs.Empty);
    }

    protected virtual void OnSetting(INode<T> node, T data) => this.OnSettingCore(node, data, true);

    protected virtual void OnSettingCore(INode<T> node, T data, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>) this._EventHandlerList[NodeTree<T>.SettingEventKey];
        if (eventHandler != null)
          eventHandler((object) node, new NodeTreeDataEventArgs<T>(data));
      }
      if (!this.IsRoot)
        NodeTree<T>.GetNodeTree(this.Root).OnSettingCore(node, data, false);
      if (!raiseValidate)
        return;
      this.OnValidate(node, data);
    }

    protected virtual void OnSetDone(INode<T> node, T data) => this.OnSetDoneCore(node, data, true);

    protected virtual void OnSetDoneCore(INode<T> node, T data, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>) this._EventHandlerList[NodeTree<T>.SetDoneEventKey];
        if (eventHandler != null)
          eventHandler((object) node, new NodeTreeDataEventArgs<T>(data));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnSetDoneCore(node, data, false);
    }

    protected virtual void OnInserting(
      INode<T> oldNode,
      NodeTreeInsertOperation operation,
      INode<T> newNode)
    {
      this.OnInsertingCore(oldNode, operation, newNode, true);
    }

    protected virtual void OnInsertingCore(
      INode<T> oldNode,
      NodeTreeInsertOperation operation,
      INode<T> newNode,
      bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>) this._EventHandlerList[NodeTree<T>.InsertingEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));
      }
      if (!this.IsRoot)
        NodeTree<T>.GetNodeTree(this.Root).OnInsertingCore(oldNode, operation, newNode, false);
      if (raiseValidate)
        this.OnValidate(oldNode, newNode.Data);
      if (!raiseValidate)
        return;
      this.OnInsertingTree(newNode);
    }

    protected virtual void OnInsertingTree(INode<T> newNode)
    {
      for (INode<T> node = newNode.Child; node != null; node = node.Next)
      {
        this.OnInsertingTree(newNode, node);
        this.OnInsertingTree(node);
      }
    }

    protected virtual void OnInsertingTree(INode<T> newNode, INode<T> child) => this.OnInsertingTreeCore(newNode, child, true);

    protected virtual void OnInsertingTreeCore(
      INode<T> newNode,
      INode<T> child,
      bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>) this._EventHandlerList[NodeTree<T>.InsertingEventKey];
        if (eventHandler != null)
          eventHandler((object) newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));
      }
      if (!this.IsRoot)
        NodeTree<T>.GetNodeTree(this.Root).OnInsertingTreeCore(newNode, child, false);
      if (!raiseValidate)
        return;
      this.OnValidate(newNode, child.Data);
    }

    protected virtual void OnInserted(
      INode<T> oldNode,
      NodeTreeInsertOperation operation,
      INode<T> newNode)
    {
      this.OnInsertedCore(oldNode, operation, newNode, true);
    }

    protected virtual void OnInsertedCore(
      INode<T> oldNode,
      NodeTreeInsertOperation operation,
      INode<T> newNode,
      bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>) this._EventHandlerList[NodeTree<T>.InsertedEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));
      }
      if (!this.IsRoot)
        NodeTree<T>.GetNodeTree(this.Root).OnInsertedCore(oldNode, operation, newNode, false);
      if (!raiseValidate)
        return;
      this.OnInsertedTree(newNode);
    }

    protected virtual void OnInsertedTree(INode<T> newNode)
    {
      for (INode<T> node = newNode.Child; node != null; node = node.Next)
      {
        this.OnInsertedTree(newNode, node);
        this.OnInsertedTree(node);
      }
    }

    protected virtual void OnInsertedTree(INode<T> newNode, INode<T> child) => this.OnInsertedTreeCore(newNode, child, true);

    protected virtual void OnInsertedTreeCore(INode<T> newNode, INode<T> child, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>) this._EventHandlerList[NodeTree<T>.InsertedEventKey];
        if (eventHandler != null)
          eventHandler((object) newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnInsertedTreeCore(newNode, child, false);
    }

    protected virtual void OnCutting(INode<T> oldNode)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler eventHandler = (EventHandler) this._EventHandlerList[NodeTree<T>.CuttingEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, EventArgs.Empty);
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnCutting(oldNode);
    }

    protected virtual void OnCutDone(INode<T> oldRoot, INode<T> oldNode)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler eventHandler = (EventHandler) this._EventHandlerList[NodeTree<T>.CutDoneEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, EventArgs.Empty);
      }
      if (this.IsTree)
        return;
      NodeTree<T>.GetNodeTree(oldRoot).OnCutDone(oldRoot, oldNode);
    }

    protected virtual void OnCopying(INode<T> oldNode, INode<T> newNode) => this.OnCopyingCore(oldNode, newNode, true);

    protected virtual void OnCopyingCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>) this._EventHandlerList[NodeTree<T>.CopyingEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeNodeEventArgs<T>(newNode));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnCopyingCore(oldNode, newNode, false);
    }

    protected virtual void OnCopied(INode<T> oldNode, INode<T> newNode) => this.OnCopiedCore(oldNode, newNode, true);

    protected virtual void OnCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>) this._EventHandlerList[NodeTree<T>.CopiedEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeNodeEventArgs<T>(newNode));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnCopiedCore(oldNode, newNode, false);
    }

    protected virtual void OnDeepCopying(INode<T> oldNode, INode<T> newNode) => this.OnDeepCopyingCore(oldNode, newNode, true);

    protected virtual void OnDeepCopyingCore(
      INode<T> oldNode,
      INode<T> newNode,
      bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>) this._EventHandlerList[NodeTree<T>.DeepCopyingEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeNodeEventArgs<T>(newNode));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnDeepCopyingCore(oldNode, newNode, false);
    }

    protected virtual void OnDeepCopied(INode<T> oldNode, INode<T> newNode) => this.OnDeepCopiedCore(oldNode, newNode, true);

    protected virtual void OnDeepCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
    {
      if (this._EventHandlerList != null)
      {
        EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>) this._EventHandlerList[NodeTree<T>.DeepCopiedEventKey];
        if (eventHandler != null)
          eventHandler((object) oldNode, new NodeTreeNodeEventArgs<T>(newNode));
      }
      if (this.IsRoot)
        return;
      NodeTree<T>.GetNodeTree(this.Root).OnDeepCopiedCore(oldNode, newNode, false);
    }

    [Serializable]
    protected class RootObject : NodeTree<T>
    {
      private int _Version;
      private IEqualityComparer<T> _DataComparer;

      protected override int Version
      {
        get => this._Version;
        set => this._Version = value;
      }

      public override IEqualityComparer<T> DataComparer
      {
        get
        {
          if (this._DataComparer == null)
            this._DataComparer = (IEqualityComparer<T>) EqualityComparer<T>.Default;
          return this._DataComparer;
        }
        set => this._DataComparer = value;
      }

      public RootObject()
      {
      }

      public RootObject(IEqualityComparer<T> dataComparer) => this._DataComparer = dataComparer;

      public override string ToString() => "ROOT: " + this.DataType.Name;

      [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("RootObjectVersion", 1);
      }

      protected RootObject(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        if (info.GetInt32("RootObjectVersion") != 1)
          throw new SerializationException("Unknown version");
      }
    }

    [XmlType("Tree")]
    public class TreeXmlSerializationAdapter
    {
      private int _Version;
      private ITree<T> _Tree;

      [XmlAttribute]
      public int Version
      {
        get => 1;
        set => this._Version = value;
      }

      [XmlIgnore]
      public ITree<T> Tree => this._Tree;

      private TreeXmlSerializationAdapter()
      {
      }

      public TreeXmlSerializationAdapter(object tag, ITree<T> tree)
      {
        if (!object.ReferenceEquals(NodeTree<T>.XmlAdapterTag, tag))
          throw new InvalidOperationException("Don't use this class");
        this._Tree = tree;
      }

      public NodeTree<T>.NodeXmlSerializationAdapter Root
      {
        get => new NodeTree<T>.NodeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, this._Tree.Root);
        set
        {
          this._Tree = NodeTree<T>.NewTree();
          this.ReformTree(this._Tree.Root, value);
        }
      }

      private void ReformTree(INode<T> parent, NodeTree<T>.NodeXmlSerializationAdapter node)
      {
        foreach (NodeTree<T>.NodeXmlSerializationAdapter child in (IEnumerable) node.Children)
          this.ReformTree(parent.AddChild(child.Data), child);
      }
    }

    [XmlType("Node")]
    public class NodeXmlSerializationAdapter
    {
      private int _Version;
      private INode<T> _Node;
      private NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection _Children = (NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection) new NodeTree<T>.NodeXmlSerializationAdapter.ChildCollection();

      [XmlAttribute]
      public int Version
      {
        get => 1;
        set => this._Version = value;
      }

      [XmlIgnore]
      public INode<T> Node => this._Node;

      private NodeXmlSerializationAdapter() => this._Node = NodeTree<T>.NewNode();

      public NodeXmlSerializationAdapter(object tag, INode<T> node)
      {
        if (!object.ReferenceEquals(NodeTree<T>.XmlAdapterTag, tag))
          throw new InvalidOperationException("Don't use this class");
        this._Node = node;
        foreach (INode<T> node1 in (IEnumerable<INode<T>>) node.DirectChildren.Nodes)
          this._Children.Add(new NodeTree<T>.NodeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, node1));
      }

      public T Data
      {
        get => this._Node.Data;
        set => NodeTree<T>.GetNodeTree(this._Node)._Data = value;
      }

      public NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection Children
      {
        get => this._Children;
        set
        {
        }
      }

      public interface IXmlCollection : ICollection, IEnumerable
      {
        NodeTree<T>.NodeXmlSerializationAdapter this[int index] { get; }

        void Add(NodeTree<T>.NodeXmlSerializationAdapter item);
      }

      private class ChildCollection : 
        List<NodeTree<T>.NodeXmlSerializationAdapter>,
        NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection,
        ICollection,
        IEnumerable
      {
      }
    }

    protected abstract class BaseEnumerableCollectionPair : IEnumerableCollectionPair<T>
    {
      private NodeTree<T> _Root;

      protected NodeTree<T> Root
      {
        get => this._Root;
        set => this._Root = value;
      }

      protected BaseEnumerableCollectionPair(NodeTree<T> root) => this._Root = root;

      public abstract IEnumerableCollection<INode<T>> Nodes { get; }

      public virtual IEnumerableCollection<T> Values => (IEnumerableCollection<T>) new NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection(this._Root.DataComparer, this.Nodes);

      protected abstract class BaseNodesEnumerableCollection : 
        IEnumerableCollection<INode<T>>,
        IEnumerable<INode<T>>,
        ICollection,
        IEnumerable,
        IEnumerator<INode<T>>,
        IDisposable,
        IEnumerator
      {
        private int _Version;
        private object _SyncRoot = new object();
        private NodeTree<T> _Root;
        private INode<T> _CurrentNode;
        private bool _BeforeFirst = true;
        private bool _AfterLast;

        protected NodeTree<T> Root
        {
          get => this._Root;
          set => this._Root = value;
        }

        protected INode<T> CurrentNode
        {
          get => this._CurrentNode;
          set => this._CurrentNode = value;
        }

        protected bool BeforeFirst
        {
          get => this._BeforeFirst;
          set => this._BeforeFirst = value;
        }

        protected bool AfterLast
        {
          get => this._AfterLast;
          set => this._AfterLast = value;
        }

        protected BaseNodesEnumerableCollection(NodeTree<T> root)
        {
          this._Root = root;
          this._CurrentNode = (INode<T>) root;
          this._Version = this._Root.Version;
        }

        ~BaseNodesEnumerableCollection() => this.Dispose(false);

        protected abstract NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy();

        protected virtual bool HasChanged => this._Root.HasChanged(this._Version);

        public void Dispose()
        {
          this.Dispose(true);
          GC.SuppressFinalize((object) this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

        public virtual IEnumerator<INode<T>> GetEnumerator() => (IEnumerator<INode<T>>) this;

        public virtual int Count
        {
          get
          {
            NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection copy = this.CreateCopy();
            int count = 0;
            foreach (INode<T> node in copy)
              ++count;
            return count;
          }
        }

        public virtual bool IsSynchronized => false;

        public virtual object SyncRoot => this._SyncRoot;

        void ICollection.CopyTo(Array array, int index)
        {
          if (array == null)
            throw new ArgumentNullException(nameof (array));
          if (array.Rank > 1)
            throw new ArgumentException("array is multidimensional", nameof (array));
          if (index < 0)
            throw new ArgumentOutOfRangeException(nameof (index));
          int count = this.Count;
          if (count > 0 && index >= array.Length)
            throw new ArgumentException("index is out of bounds", nameof (index));
          if (index + count > array.Length)
            throw new ArgumentException("Not enough space in array", nameof (array));
          foreach (INode<T> node in this.CreateCopy())
            array.SetValue((object) node, index++);
        }

        public virtual void CopyTo(T[] array, int index) => ((ICollection) this).CopyTo((Array) array, index);

        public virtual bool Contains(INode<T> item)
        {
          NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection copy = this.CreateCopy();
          IEqualityComparer<INode<T>> equalityComparer = (IEqualityComparer<INode<T>>) EqualityComparer<INode<T>>.Default;
          foreach (INode<T> x in copy)
          {
            if (equalityComparer.Equals(x, item))
              return true;
          }
          return false;
        }

        object IEnumerator.Current => (object) this.Current;

        public virtual void Reset()
        {
          if (this.HasChanged)
            throw new InvalidOperationException("Tree has been modified.");
          this._CurrentNode = (INode<T>) this._Root;
          this._BeforeFirst = true;
          this._AfterLast = false;
        }

        public virtual bool MoveNext()
        {
          if (this.HasChanged)
            throw new InvalidOperationException("Tree has been modified.");
          this._BeforeFirst = false;
          return true;
        }

        public virtual INode<T> Current
        {
          get
          {
            if (this._BeforeFirst)
              throw new InvalidOperationException("Enumeration has not started.");
            if (this._AfterLast)
              throw new InvalidOperationException("Enumeration has finished.");
            return this._CurrentNode;
          }
        }
      }

      private class ValuesEnumerableCollection : 
        IEnumerableCollection<T>,
        IEnumerable<T>,
        ICollection,
        IEnumerable,
        IEnumerator<T>,
        IDisposable,
        IEnumerator
      {
        private IEqualityComparer<T> _DataComparer;
        private IEnumerableCollection<INode<T>> _Nodes;
        private IEnumerator<INode<T>> _Enumerator;

        public ValuesEnumerableCollection(
          IEqualityComparer<T> dataComparer,
          IEnumerableCollection<INode<T>> nodes)
        {
          this._DataComparer = dataComparer;
          this._Nodes = nodes;
          this._Enumerator = this._Nodes.GetEnumerator();
        }

        protected ValuesEnumerableCollection(
          NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection o)
        {
          this._Nodes = o._Nodes;
          this._Enumerator = this._Nodes.GetEnumerator();
        }

        protected virtual NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection CreateCopy() => new NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection(this);

        ~ValuesEnumerableCollection() => this.Dispose(false);

        public void Dispose()
        {
          this.Dispose(true);
          GC.SuppressFinalize((object) this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

        public virtual IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this;

        public virtual int Count => this._Nodes.Count;

        public virtual bool IsSynchronized => false;

        public virtual object SyncRoot => this._Nodes.SyncRoot;

        public virtual void CopyTo(Array array, int index)
        {
          if (array == null)
            throw new ArgumentNullException(nameof (array));
          if (array.Rank > 1)
            throw new ArgumentException("array is multidimensional", nameof (array));
          if (index < 0)
            throw new ArgumentOutOfRangeException(nameof (index));
          int count = this.Count;
          if (count > 0 && index >= array.Length)
            throw new ArgumentException("index is out of bounds", nameof (index));
          if (index + count > array.Length)
            throw new ArgumentException("Not enough space in array", nameof (array));
          foreach (T obj in this.CreateCopy())
            array.SetValue((object) obj, index++);
        }

        public virtual bool Contains(T item)
        {
          foreach (T x in this.CreateCopy())
          {
            if (this._DataComparer.Equals(x, item))
              return true;
          }
          return false;
        }

        object IEnumerator.Current => (object) this.Current;

        public virtual void Reset() => this._Enumerator.Reset();

        public virtual bool MoveNext() => this._Enumerator.MoveNext();

        public virtual T Current
        {
          get
          {
            if (this._Enumerator == null)
              return default (T);
            return this._Enumerator.Current == null ? default (T) : this._Enumerator.Current.Data;
          }
        }
      }
    }

    protected class AllEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
    {
      public AllEnumerator(NodeTree<T> root)
        : base(root)
      {
      }

      public override IEnumerableCollection<INode<T>> Nodes => (IEnumerableCollection<INode<T>>) new NodeTree<T>.AllEnumerator.NodesEnumerableCollection(this.Root);

      protected class NodesEnumerableCollection : 
        NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
      {
        private bool _First = true;

        public NodesEnumerableCollection(NodeTree<T> root)
          : base(root)
        {
        }

        protected NodesEnumerableCollection(
          NodeTree<T>.AllEnumerator.NodesEnumerableCollection o)
          : base(o.Root)
        {
        }

        protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy() => (NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection) new NodeTree<T>.AllEnumerator.NodesEnumerableCollection(this);

        public override void Reset()
        {
          base.Reset();
          this._First = true;
        }

        public override bool MoveNext()
        {
          if (base.MoveNext())
          {
            if (this.CurrentNode == null)
              throw new InvalidOperationException("Current is null");
            if (this.CurrentNode.IsRoot)
            {
              this.CurrentNode = this.CurrentNode.Child;
              if (this.CurrentNode == null)
                goto label_13;
            }
            if (this._First)
            {
              this._First = false;
              return true;
            }
            if (this.CurrentNode.Child != null)
            {
              this.CurrentNode = this.CurrentNode.Child;
              return true;
            }
            for (; this.CurrentNode.Parent != null && this.CurrentNode != this.Root; this.CurrentNode = this.CurrentNode.Parent)
            {
              if (this.CurrentNode.Next != null)
              {
                this.CurrentNode = this.CurrentNode.Next;
                return true;
              }
            }
          }
label_13:
          this.AfterLast = true;
          return false;
        }
      }
    }

    private class AllChildrenEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
    {
      public AllChildrenEnumerator(NodeTree<T> root)
        : base(root)
      {
      }

      public override IEnumerableCollection<INode<T>> Nodes => (IEnumerableCollection<INode<T>>) new NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection(this.Root);

      protected class NodesEnumerableCollection : 
        NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
      {
        public NodesEnumerableCollection(NodeTree<T> root)
          : base(root)
        {
        }

        protected NodesEnumerableCollection(
          NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection o)
          : base(o.Root)
        {
        }

        protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy() => (NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection) new NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection(this);

        public override bool MoveNext()
        {
          if (base.MoveNext())
          {
            if (this.CurrentNode == null)
              throw new InvalidOperationException("Current is null");
            if (this.CurrentNode.Child != null)
            {
              this.CurrentNode = this.CurrentNode.Child;
              return true;
            }
            for (; this.CurrentNode.Parent != null && this.CurrentNode != this.Root; this.CurrentNode = this.CurrentNode.Parent)
            {
              if (this.CurrentNode.Next != null)
              {
                this.CurrentNode = this.CurrentNode.Next;
                return true;
              }
            }
          }
          this.AfterLast = true;
          return false;
        }
      }
    }

    private class DirectChildrenEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
    {
      public DirectChildrenEnumerator(NodeTree<T> root)
        : base(root)
      {
      }

      public override IEnumerableCollection<INode<T>> Nodes => (IEnumerableCollection<INode<T>>) new NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection(this.Root);

      protected class NodesEnumerableCollection : 
        NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
      {
        public NodesEnumerableCollection(NodeTree<T> root)
          : base(root)
        {
        }

        protected NodesEnumerableCollection(
          NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection o)
          : base(o.Root)
        {
        }

        protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy() => (NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection) new NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection(this);

        public override int Count => this.Root.DirectChildCount;

        public override bool MoveNext()
        {
          if (base.MoveNext())
          {
            if (this.CurrentNode == null)
              throw new InvalidOperationException("Current is null");
            if (this.CurrentNode == this.Root)
              this.CurrentNode = this.Root.Child;
            else
              this.CurrentNode = this.CurrentNode.Next;
            if (this.CurrentNode != null)
              return true;
          }
          this.AfterLast = true;
          return false;
        }
      }
    }

    private class DirectChildrenInReverseEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
    {
      public DirectChildrenInReverseEnumerator(NodeTree<T> root)
        : base(root)
      {
      }

      public override IEnumerableCollection<INode<T>> Nodes => (IEnumerableCollection<INode<T>>) new NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection(this.Root);

      protected class NodesEnumerableCollection : 
        NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
      {
        public NodesEnumerableCollection(NodeTree<T> root)
          : base(root)
        {
        }

        protected NodesEnumerableCollection(
          NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection o)
          : base(o.Root)
        {
        }

        protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy() => (NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection) new NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection(this);

        public override int Count => this.Root.DirectChildCount;

        public override bool MoveNext()
        {
          if (base.MoveNext())
          {
            if (this.CurrentNode == null)
              throw new InvalidOperationException("Current is null");
            if (this.CurrentNode == this.Root)
              this.CurrentNode = this.Root.LastChild;
            else
              this.CurrentNode = this.CurrentNode.Previous;
            if (this.CurrentNode != null)
              return true;
          }
          this.AfterLast = true;
          return false;
        }
      }
    }
  }
}

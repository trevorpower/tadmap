using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using Tad.DataPortal;

namespace Tad
{
    /// <summary>
  /// This is the base class from which most business collections
  /// or lists will be derived.
  /// </summary>
  /// <typeparam name="T">Type of the business object being defined.</typeparam>
  /// <typeparam name="C">Type of the child objects contained in the list.</typeparam>
  [System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
  [Serializable()]
  public abstract class BusinessListBase<T, C> :
      Core.ExtendedBindingList<C>,
      Core.IEditableCollection, Core.IUndoableObject, Core.ISavable, Core.IParent
    where T : BusinessListBase<T, C>
    where C : Core.IEditableBusinessObject
  {

    #region Constructors

    /// <summary>
    /// Creates an instance of the object.
    /// </summary>
    protected BusinessListBase()
    {
      Initialize();
    }

    #endregion

    #region Initialize

    /// <summary>
    /// Override this method to set up event handlers so user
    /// code in a partial class can respond to events raised by
    /// generated code.
    /// </summary>
    protected virtual void Initialize()
    { /* allows subclass to initialize events before any other activity occurs */ }

    #endregion

    #region IsDirty, IsValid, IsSavable

    /// <summary>
    /// Gets a value indicating whether this object's data has been changed.
    /// </summary>
    public bool IsDirty
    {
      get
      {
        // any non-new deletions make us dirty
        foreach (C item in DeletedList)
          if (!item.IsNew)
            return true;

        // run through all the child objects
        // and if any are dirty then then
        // collection is dirty
        foreach (C child in this)
          if (child.IsDirty)
            return true;
        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this object is currently in
    /// a valid state (has no broken validation rules).
    /// </summary>
    public virtual bool IsValid
    {
      get
      {
        // run through all the child objects
        // and if any are invalid then the
        // collection is invalid
        foreach (C child in this)
          if (!child.IsValid)
            return false;
        return true;
      }
    }

    /// <summary>
    /// Returns <see langword="true" /> if this object is both dirty and valid.
    /// </summary>
    /// <returns>A value indicating if this object is both dirty and valid.</returns>
    [Browsable(false)]
    public virtual bool IsSavable
    {
      get { return (IsDirty && IsValid); }
    }

    #endregion

    #region Begin/Cancel/ApplyEdit

    /// <summary>
    /// Starts a nested edit on the object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When this method is called the object takes a snapshot of
    /// its current state (the values of its variables). This snapshot
    /// can be restored by calling <see cref="CancelEdit" />
    /// or committed by calling <see cref="ApplyEdit" />.
    /// </para><para>
    /// This is a nested operation. Each call to BeginEdit adds a new
    /// snapshot of the object's state to a stack. You should ensure that 
    /// for each call to BeginEdit there is a corresponding call to either 
    /// CancelEdit or ApplyEdit to remove that snapshot from the stack.
    /// </para><para>
    /// See Chapters 2 and 3 for details on n-level undo and state stacking.
    /// </para><para>
    /// This method triggers the copying of all child object states.
    /// </para>
    /// </remarks>
    public void BeginEdit()
    {
      if (this.IsChild)
        throw new NotSupportedException("Cannot being edit a child.");
    }

    /// <summary>
    /// Cancels the current edit process, restoring the object's state to
    /// its previous values.
    /// </summary>
    /// <remarks>
    /// Calling this method causes the most recently taken snapshot of the 
    /// object's state to be restored. This resets the object's values
    /// to the point of the last <see cref="BeginEdit" />
    /// call.
    /// <para>
    /// This method triggers an undo in all child objects.
    /// </para>
    /// </remarks>
    public void CancelEdit()
    {
      if (this.IsChild)
        throw new NotSupportedException("You cannot cancel the edit of a child!");
    }

    /// <summary>
    /// Commits the current edit process.
    /// </summary>
    /// <remarks>
    /// Calling this method causes the most recently taken snapshot of the 
    /// object's state to be discarded, thus committing any changes made
    /// to the object's state since the last 
    /// <see cref="BeginEdit" /> call.
    /// <para>
    /// This method triggers an <see cref="Core.BusinessBase.ApplyEdit"/>
    ///  in all child objects.
    /// </para>
    /// </remarks>
    public void ApplyEdit()
    {
      if (this.IsChild)
        throw new NotSupportedException("You cannot apply the edit of a child!");
    }

    void Core.IParent.ApplyEditChild(Core.IEditableBusinessObject child)
    {
      EditChildComplete(child);
    }

    /// <summary>
    /// Override this method to be notified when a child object's
    /// <see cref="Core.BusinessBase.ApplyEdit" /> method has
    /// completed.
    /// </summary>
    /// <param name="child">The child object that was edited.</param>
    protected virtual void EditChildComplete(Core.IEditableBusinessObject child)
    {

      // do nothing, we don't really care
      // when a child has its edits applied
    }

    #endregion

    #region N-level undo

    void Core.IUndoableObject.CopyState(int parentEditLevel)
    {
    }

    void Core.IUndoableObject.UndoChanges(int parentEditLevel)
    {
    }

    void Core.IUndoableObject.AcceptChanges(int parentEditLevel)
    {
    }

    #endregion

    #region Delete and Undelete child

    private List<C> _deletedList;

    /// <summary>
    /// A collection containing all child objects marked
    /// for deletion.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
      "Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected List<C> DeletedList
    {
      get 
      { 
        if (_deletedList == null)
          _deletedList = new List<C>();
        return _deletedList; 
      }
    }

    private void DeleteChild(C child)
    {
      // mark the object as deleted
      child.DeleteChild();
      // and add it to the deleted collection for storage
      DeletedList.Add(child);
    }

 

    /// <summary>
    /// Returns <see langword="true"/> if the internal deleted list
    /// contains the specified child object.
    /// </summary>
    /// <param name="item">Child object to check.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool ContainsDeleted(C item)
    {
      return DeletedList.Contains(item);
    }

    #endregion

    #region Insert, Remove, Clear

    /// <summary>
    /// This method is called by a child object when it
    /// wants to be removed from the collection.
    /// </summary>
    /// <param name="child">The child object to remove.</param>
    void Core.IEditableCollection.RemoveChild(Tad.Core.IEditableBusinessObject child)
    {
      Remove((C)child);
    }

    /// <summary>
    /// This method is called by a child object when it
    /// wants to be removed from the collection.
    /// </summary>
    /// <param name="child">The child object to remove.</param>
    void Core.IParent.RemoveChild(Tad.Core.IEditableBusinessObject child)
    {
      Remove((C)child);
    }

    /// <summary>
    /// Sets the edit level of the child object as it is added.
    /// </summary>
    /// <param name="index">Index of the item to insert.</param>
    /// <param name="item">Item to insert.</param>
    protected override void InsertItem(int index, C item)
    {
      // set parent reference
      item.SetParent(this);

      base.InsertItem(index, item);
    }

    /// <summary>
    /// Marks the child object for deletion and moves it to
    /// the collection of deleted objects.
    /// </summary>
    /// <param name="index">Index of the item to remove.</param>
    protected override void RemoveItem(int index)
    {
      // when an object is 'removed' it is really
      // being deleted, so do the deletion work
      C child = this[index];
      bool oldRaiseListChangedEvents = this.RaiseListChangedEvents;
      try
      {
        this.RaiseListChangedEvents = false;
        base.RemoveItem(index);
      }
      finally
      {
        this.RaiseListChangedEvents = oldRaiseListChangedEvents;
      }
      
      if (RaiseListChangedEvents)
        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
    }

    private void CopyToDeletedList(C child)
    {
      DeleteChild(child);
      INotifyPropertyChanged c = child as INotifyPropertyChanged;
      if (c != null)
        c.PropertyChanged -= new PropertyChangedEventHandler(Child_PropertyChanged);
    }

    /// <summary>
    /// Clears the collection, moving all active
    /// items to the deleted list.
    /// </summary>
    protected override void ClearItems()
    {
      while (base.Count > 0) RemoveItem(0);
      base.ClearItems();
    }

    /// <summary>
    /// Replaces the item at the specified index with
    /// the specified item, first moving the original
    /// item to the deleted list.
    /// </summary>
    /// <param name="index">The zero-based index of the item to replace.</param>
    /// <param name="item">
    /// The new value for the item at the specified index. 
    /// The value can be null for reference types.
    /// </param>
    /// <remarks></remarks>
    protected override void SetItem(int index, C item)
    {
      C child = default(C);
      if (!(ReferenceEquals((C)(this[index]), item)))
        child = this[index];
      // replace the original object with this new
      // object
      bool oldRaiseListChangedEvents = this.RaiseListChangedEvents;
      try
      {
        this.RaiseListChangedEvents = false;
        // set parent reference
        item.SetParent(this);

        base.SetItem(index, item);
      }
      finally
      {
        this.RaiseListChangedEvents = oldRaiseListChangedEvents;
      }
      if (child != null)
        CopyToDeletedList(child);
      if (RaiseListChangedEvents)
        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
    }

    #endregion

    #region IsChild

    private bool _isChild = false;

    /// <summary>
    /// Indicates whether this collection object is a child object.
    /// </summary>
    /// <returns>True if this is a child object.</returns>
    protected bool IsChild
    {
      get { return _isChild; }
    }

    /// <summary>
    /// Marks the object as being a child object.
    /// </summary>
    /// <remarks>
    /// <para>
    /// By default all business objects are 'parent' objects. This means
    /// that they can be directly retrieved and updated into the database.
    /// </para><para>
    /// We often also need child objects. These are objects which are contained
    /// within other objects. For instance, a parent Invoice object will contain
    /// child LineItem objects.
    /// </para><para>
    /// To create a child object, the MarkAsChild method must be called as the
    /// object is created. Please see Chapter 7 for details on the use of the
    /// MarkAsChild method.
    /// </para>
    /// </remarks>
    protected void MarkAsChild()
    {
      _isChild = true;
    }

    #endregion

 
    #region Cascade Child events

    private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (RaiseListChangedEvents)
      {
        for (int index = 0; index < Count; index++)
        {
          if (ReferenceEquals(this[index], sender))
          {
            PropertyDescriptor descriptor = GetPropertyDescriptor(e.PropertyName);
            if (descriptor != null)
              OnListChanged(new ListChangedEventArgs(
                ListChangedType.ItemChanged, index, GetPropertyDescriptor(e.PropertyName)));
            else
              OnListChanged(new ListChangedEventArgs(
                ListChangedType.ItemChanged, index));
            return;
          }
        }
      }
    }

    private static PropertyDescriptorCollection _propertyDescriptors;

    private PropertyDescriptor GetPropertyDescriptor(string propertyName)
    {
      if (_propertyDescriptors == null)
        _propertyDescriptors = TypeDescriptor.GetProperties(typeof(C));
      PropertyDescriptor result = null;
      foreach (PropertyDescriptor desc in _propertyDescriptors)
        if (desc.Name == propertyName)
        {
          result = desc;
          break;
        }
      return result;
    }

    #endregion

    #region Serialization Notification

    [OnDeserialized()]
    private void OnDeserializedHandler(StreamingContext context)
    {
      OnDeserialized(context);
      foreach (Core.IEditableBusinessObject child in this)
      {
        child.SetParent(this);
        INotifyPropertyChanged c = child as INotifyPropertyChanged;
        if (c != null)
          c.PropertyChanged += new PropertyChangedEventHandler(Child_PropertyChanged);
      }
      foreach (Core.IEditableBusinessObject child in DeletedList)
        child.SetParent(this);
    }

    /// <summary>
    /// This method is called on a newly deserialized object
    /// after deserialization is complete.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDeserialized(StreamingContext context)
    {
      // do nothing - this is here so a subclass
      // could override if needed
    }

    #endregion

    #region Data Access

    /// <summary>
    /// Saves the object to the database.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method starts the save operation, causing the all child
    /// objects to be inserted, updated or deleted within the database based on the
    /// each object's current state.
    /// </para><para>
    /// All this is contingent on <see cref="IsDirty" />. If
    /// this value is <see langword="false"/>, no data operation occurs. 
    /// It is also contingent on <see cref="IsValid" />. If this value is 
    /// <see langword="false"/> an exception will be thrown to 
    /// indicate that the UI attempted to save an invalid object.
    /// </para><para>
    /// It is important to note that this method returns a new version of the
    /// business collection that contains any data updated during the save operation.
    /// You MUST update all object references to use this new version of the
    /// business collection in order to have access to the correct object data.
    /// </para><para>
    /// You can override this method to add your own custom behaviors to the save
    /// operation. For instance, you may add some security checks to make sure
    /// the user can save the object. If all security checks pass, you would then
    /// invoke the base Save method via <c>MyBase.Save()</c>.
    /// </para>
    /// </remarks>
    /// <returns>A new object containing the saved values.</returns>
    public virtual T Save()
    {
      T result;
      if (this.IsChild)
        throw new NotSupportedException("Cannot save a child");

      if (!IsValid)
        throw new Validation.ValidationException("Cannot save an invalid object");

      if (IsDirty)
        result = (T)DataPortal.Client.DataPortal.Update(this);
      else
        result = (T)this;
      OnSaved(result);
      return result;
    }

    /// <summary>
    /// Override this method to load a new business object with default
    /// values from the database.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    protected virtual void DataPortal_Create()
    {
      throw new NotSupportedException("Create not supported");
    }

    /// <summary>
    /// Override this method to allow retrieval of an existing business
    /// object based on data in the database.
    /// </summary>
    /// <param name="criteria">An object containing criteria values to identify the object.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    protected virtual void DataPortal_Fetch(object criteria)
    {
      throw new NotSupportedException("Fetch not supported");
    }

    /// <summary>
    /// Override this method to allow update of a business
    /// object.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    protected virtual void DataPortal_Update()
    {
      throw new NotSupportedException("Update not supported");
    }

    /// <summary>
    /// Override this method to allow immediate deletion of a business object.
    /// </summary>
    /// <param name="criteria">An object containing criteria values to identify the object.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    protected virtual void DataPortal_Delete(object criteria)
    {
      throw new NotSupportedException("Delete not supported");
    }

    /// <summary>
    /// Called by the server-side DataPortal prior to calling the 
    /// requested DataPortal_xyz method.
    /// </summary>
    /// <param name="e">The DataPortalContext object passed to the DataPortal.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DataPortal_OnDataPortalInvoke(DataPortalEventArgs e)
    {

    }

    /// <summary>
    /// Called by the server-side DataPortal after calling the 
    /// requested DataPortal_xyz method.
    /// </summary>
    /// <param name="e">The DataPortalContext object passed to the DataPortal.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DataPortal_OnDataPortalInvokeComplete(DataPortalEventArgs e)
    {

    }

    /// <summary>
    /// Called by the server-side DataPortal if an exception
    /// occurs during data access.
    /// </summary>
    /// <param name="e">The DataPortalContext object passed to the DataPortal.</param>
    /// <param name="ex">The Exception thrown during data access.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", MessageId = "Member")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void DataPortal_OnDataPortalException(DataPortalEventArgs e, Exception ex)
    {

    }

    #endregion

    #region ISavable Members

    object Tad.Core.ISavable.Save()
    {
      return Save();
    }

    void Tad.Core.ISavable.SaveComplete(object newObject)
    {
      OnSaved((T)newObject);
    }

    /// <summary>
    /// Raises the <see cref="Saved"/> event, indicating that the
    /// object has been saved, and providing a reference
    /// to the new object instance.
    /// </summary>
    /// <param name="newObject">The new object instance.</param>
    [System.ComponentModel.EditorBrowsable(EditorBrowsableState.Advanced)]
    protected void OnSaved(T newObject)
    {

    }

    #endregion

  }
}

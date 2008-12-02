﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



[System.Data.Linq.Mapping.DatabaseAttribute(Name="tadmap")]
public partial class TadmapDb : System.Data.Linq.DataContext
{
	
	private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
	
  #region Extensibility Method Definitions
  partial void OnCreated();
  partial void InsertUser(User instance);
  partial void UpdateUser(User instance);
  partial void DeleteUser(User instance);
  partial void InsertUserRole(UserRole instance);
  partial void UpdateUserRole(UserRole instance);
  partial void DeleteUserRole(UserRole instance);
  partial void InsertUserOpenId(UserOpenId instance);
  partial void UpdateUserOpenId(UserOpenId instance);
  partial void DeleteUserOpenId(UserOpenId instance);
  partial void InsertUserImage(UserImage instance);
  partial void UpdateUserImage(UserImage instance);
  partial void DeleteUserImage(UserImage instance);
  #endregion
	
	public TadmapDb() : 
			base(global::System.Configuration.ConfigurationManager.ConnectionStrings["tadmapConnectionString"].ConnectionString, mappingSource)
	{
		OnCreated();
	}
	
	public TadmapDb(string connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TadmapDb(System.Data.IDbConnection connection) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public TadmapDb(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}

   public TadmapDb(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
			base(connection, mappingSource)
	{
		OnCreated();
	}
	
	public System.Data.Linq.Table<User> Users
	{
		get
		{
			return this.GetTable<User>();
		}
	}
	
	public System.Data.Linq.Table<UserRole> UserRoles
	{
		get
		{
			return this.GetTable<UserRole>();
		}
	}
	
	public System.Data.Linq.Table<UserOpenId> UserOpenIds
	{
		get
		{
			return this.GetTable<UserOpenId>();
		}
	}
	
	public System.Data.Linq.Table<UserImage> UserImages
	{
		get
		{
			return this.GetTable<UserImage>();
		}
	}
}

[Table(Name="dbo.Users")]
public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _Id;
	
	private string _Name;
	
	private EntitySet<UserRole> _UserRoles;
	
	private EntitySet<UserOpenId> _OpenIds;
	
	private EntitySet<UserImage> _UserImages;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
	
	public User()
	{
		this._UserRoles = new EntitySet<UserRole>(new Action<UserRole>(this.attach_UserRoles), new Action<UserRole>(this.detach_UserRoles));
		this._OpenIds = new EntitySet<UserOpenId>(new Action<UserOpenId>(this.attach_OpenIds), new Action<UserOpenId>(this.detach_OpenIds));
		this._UserImages = new EntitySet<UserImage>(new Action<UserImage>(this.attach_UserImages), new Action<UserImage>(this.detach_UserImages));
		OnCreated();
	}
	
	[Column(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[Column(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
	public string Name
	{
		get
		{
			return this._Name;
		}
		set
		{
			if ((this._Name != value))
			{
				this.OnNameChanging(value);
				this.SendPropertyChanging();
				this._Name = value;
				this.SendPropertyChanged("Name");
				this.OnNameChanged();
			}
		}
	}
	
	[Association(Name="User_UserRole", Storage="_UserRoles", ThisKey="Id", OtherKey="UserId")]
	public EntitySet<UserRole> UserRoles
	{
		get
		{
			return this._UserRoles;
		}
		set
		{
			this._UserRoles.Assign(value);
		}
	}
	
	[Association(Name="User_UserOpenId", Storage="_OpenIds", ThisKey="Id", OtherKey="UserId")]
	public EntitySet<UserOpenId> UserOpenIds
	{
		get
		{
			return this._OpenIds;
		}
		set
		{
			this._OpenIds.Assign(value);
		}
	}
	
	[Association(Name="User_Image", Storage="_UserImages", ThisKey="Id", OtherKey="UserId")]
	public EntitySet<UserImage> UserImages
	{
		get
		{
			return this._UserImages;
		}
		set
		{
			this._UserImages.Assign(value);
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
	
	private void attach_UserRoles(UserRole entity)
	{
		this.SendPropertyChanging();
		entity.User = this;
	}
	
	private void detach_UserRoles(UserRole entity)
	{
		this.SendPropertyChanging();
		entity.User = null;
	}
	
	private void attach_OpenIds(UserOpenId entity)
	{
		this.SendPropertyChanging();
		entity.User = this;
	}
	
	private void detach_OpenIds(UserOpenId entity)
	{
		this.SendPropertyChanging();
		entity.User = null;
	}
	
	private void attach_UserImages(UserImage entity)
	{
		this.SendPropertyChanging();
		entity.User = this;
	}
	
	private void detach_UserImages(UserImage entity)
	{
		this.SendPropertyChanging();
		entity.User = null;
	}
}

[Table(Name="dbo.UserRoles")]
public partial class UserRole : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _UserId;
	
	private string _Role;
	
	private EntityRef<User> _User;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnRoleChanging(string value);
    partial void OnRoleChanged();
    #endregion
	
	public UserRole()
	{
		this._User = default(EntityRef<User>);
		OnCreated();
	}
	
	[Column(Storage="_UserId", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid UserId
	{
		get
		{
			return this._UserId;
		}
		set
		{
			if ((this._UserId != value))
			{
				if (this._User.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnUserIdChanging(value);
				this.SendPropertyChanging();
				this._UserId = value;
				this.SendPropertyChanged("UserId");
				this.OnUserIdChanged();
			}
		}
	}
	
	[Column(Storage="_Role", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
	public string Role
	{
		get
		{
			return this._Role;
		}
		set
		{
			if ((this._Role != value))
			{
				this.OnRoleChanging(value);
				this.SendPropertyChanging();
				this._Role = value;
				this.SendPropertyChanged("Role");
				this.OnRoleChanged();
			}
		}
	}
	
	[Association(Name="User_UserRole", Storage="_User", ThisKey="UserId", OtherKey="Id", IsForeignKey=true)]
	public User User
	{
		get
		{
			return this._User.Entity;
		}
		set
		{
			User previousValue = this._User.Entity;
			if (((previousValue != value) 
						|| (this._User.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._User.Entity = null;
					previousValue.UserRoles.Remove(this);
				}
				this._User.Entity = value;
				if ((value != null))
				{
					value.UserRoles.Add(this);
					this._UserId = value.Id;
				}
				else
				{
					this._UserId = default(System.Guid);
				}
				this.SendPropertyChanged("User");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="dbo.OpenIds")]
public partial class UserOpenId : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private string _OpenIdUrl;
	
	private System.Guid _UserId;
	
	private EntityRef<User> _User;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnOpenIdUrlChanging(string value);
    partial void OnOpenIdUrlChanged();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    #endregion
	
	public UserOpenId()
	{
		this._User = default(EntityRef<User>);
		OnCreated();
	}
	
	[Column(Storage="_OpenIdUrl", DbType="VarChar(255) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
	public string OpenIdUrl
	{
		get
		{
			return this._OpenIdUrl;
		}
		set
		{
			if ((this._OpenIdUrl != value))
			{
				this.OnOpenIdUrlChanging(value);
				this.SendPropertyChanging();
				this._OpenIdUrl = value;
				this.SendPropertyChanged("OpenIdUrl");
				this.OnOpenIdUrlChanged();
			}
		}
	}
	
	[Column(Storage="_UserId", DbType="UniqueIdentifier NOT NULL")]
	public System.Guid UserId
	{
		get
		{
			return this._UserId;
		}
		set
		{
			if ((this._UserId != value))
			{
				if (this._User.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnUserIdChanging(value);
				this.SendPropertyChanging();
				this._UserId = value;
				this.SendPropertyChanged("UserId");
				this.OnUserIdChanged();
			}
		}
	}
	
	[Association(Name="User_UserOpenId", Storage="_User", ThisKey="UserId", OtherKey="Id", IsForeignKey=true)]
	public User User
	{
		get
		{
			return this._User.Entity;
		}
		set
		{
			User previousValue = this._User.Entity;
			if (((previousValue != value) 
						|| (this._User.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._User.Entity = null;
					previousValue.UserOpenIds.Remove(this);
				}
				this._User.Entity = value;
				if ((value != null))
				{
					value.UserOpenIds.Add(this);
					this._UserId = value.Id;
				}
				else
				{
					this._UserId = default(System.Guid);
				}
				this.SendPropertyChanged("User");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

[Table(Name="dbo.Images")]
public partial class UserImage : INotifyPropertyChanging, INotifyPropertyChanged
{
	
	private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
	
	private System.Guid _Id;
	
	private string _Title;
	
	private string _Description;
	
	private System.Guid _UserId;
	
	private string _Key;
	
	private System.Nullable<short> _ZoomLevels;
	
	private System.Nullable<short> _TileSize;
	
	private System.DateTime _DateAdded;
	
	private byte _ImageSet;
	
	private byte _OffensiveCount;
	
	private byte _Privacy;
	
	private EntityRef<User> _User;
	
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(System.Guid value);
    partial void OnIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnUserIdChanging(System.Guid value);
    partial void OnUserIdChanged();
    partial void OnKeyChanging(string value);
    partial void OnKeyChanged();
    partial void OnZoomLevelsChanging(System.Nullable<short> value);
    partial void OnZoomLevelsChanged();
    partial void OnTileSizeChanging(System.Nullable<short> value);
    partial void OnTileSizeChanged();
    partial void OnDateAddedChanging(System.DateTime value);
    partial void OnDateAddedChanged();
    partial void OnImageSetChanging(byte value);
    partial void OnImageSetChanged();
    partial void OnOffensiveCountChanging(byte value);
    partial void OnOffensiveCountChanged();
    partial void OnPrivacyChanging(byte value);
    partial void OnPrivacyChanged();
    #endregion
	
	public UserImage()
	{
		this._User = default(EntityRef<User>);
		OnCreated();
	}
	
	[Column(Storage="_Id", DbType="UniqueIdentifier NOT NULL", IsPrimaryKey=true)]
	public System.Guid Id
	{
		get
		{
			return this._Id;
		}
		set
		{
			if ((this._Id != value))
			{
				this.OnIdChanging(value);
				this.SendPropertyChanging();
				this._Id = value;
				this.SendPropertyChanged("Id");
				this.OnIdChanged();
			}
		}
	}
	
	[Column(Storage="_Title", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
	public string Title
	{
		get
		{
			return this._Title;
		}
		set
		{
			if ((this._Title != value))
			{
				this.OnTitleChanging(value);
				this.SendPropertyChanging();
				this._Title = value;
				this.SendPropertyChanged("Title");
				this.OnTitleChanged();
			}
		}
	}
	
	[Column(Storage="_Description", DbType="NVarChar(1024) NOT NULL", CanBeNull=false)]
	public string Description
	{
		get
		{
			return this._Description;
		}
		set
		{
			if ((this._Description != value))
			{
				this.OnDescriptionChanging(value);
				this.SendPropertyChanging();
				this._Description = value;
				this.SendPropertyChanged("Description");
				this.OnDescriptionChanged();
			}
		}
	}
	
	[Column(Storage="_UserId", DbType="UniqueIdentifier NOT NULL")]
	public System.Guid UserId
	{
		get
		{
			return this._UserId;
		}
		set
		{
			if ((this._UserId != value))
			{
				if (this._User.HasLoadedOrAssignedValue)
				{
					throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
				}
				this.OnUserIdChanging(value);
				this.SendPropertyChanging();
				this._UserId = value;
				this.SendPropertyChanged("UserId");
				this.OnUserIdChanged();
			}
		}
	}
	
	[Column(Name="[Key]", Storage="_Key", DbType="NVarChar(512) NOT NULL", CanBeNull=false)]
	public string Key
	{
		get
		{
			return this._Key;
		}
		set
		{
			if ((this._Key != value))
			{
				this.OnKeyChanging(value);
				this.SendPropertyChanging();
				this._Key = value;
				this.SendPropertyChanged("Key");
				this.OnKeyChanged();
			}
		}
	}
	
	[Column(Storage="_ZoomLevels", DbType="SmallInt")]
	public System.Nullable<short> ZoomLevels
	{
		get
		{
			return this._ZoomLevels;
		}
		set
		{
			if ((this._ZoomLevels != value))
			{
				this.OnZoomLevelsChanging(value);
				this.SendPropertyChanging();
				this._ZoomLevels = value;
				this.SendPropertyChanged("ZoomLevels");
				this.OnZoomLevelsChanged();
			}
		}
	}
	
	[Column(Storage="_TileSize", DbType="SmallInt")]
	public System.Nullable<short> TileSize
	{
		get
		{
			return this._TileSize;
		}
		set
		{
			if ((this._TileSize != value))
			{
				this.OnTileSizeChanging(value);
				this.SendPropertyChanging();
				this._TileSize = value;
				this.SendPropertyChanged("TileSize");
				this.OnTileSizeChanged();
			}
		}
	}
	
	[Column(Storage="_DateAdded", DbType="DateTime NOT NULL")]
	public System.DateTime DateAdded
	{
		get
		{
			return this._DateAdded;
		}
		set
		{
			if ((this._DateAdded != value))
			{
				this.OnDateAddedChanging(value);
				this.SendPropertyChanging();
				this._DateAdded = value;
				this.SendPropertyChanged("DateAdded");
				this.OnDateAddedChanged();
			}
		}
	}
	
	[Column(Storage="_ImageSet", DbType="TinyInt NOT NULL")]
	public byte ImageSet
	{
		get
		{
			return this._ImageSet;
		}
		set
		{
			if ((this._ImageSet != value))
			{
				this.OnImageSetChanging(value);
				this.SendPropertyChanging();
				this._ImageSet = value;
				this.SendPropertyChanged("ImageSet");
				this.OnImageSetChanged();
			}
		}
	}
	
	[Column(Storage="_OffensiveCount", DbType="TinyInt NOT NULL")]
	public byte OffensiveCount
	{
		get
		{
			return this._OffensiveCount;
		}
		set
		{
			if ((this._OffensiveCount != value))
			{
				this.OnOffensiveCountChanging(value);
				this.SendPropertyChanging();
				this._OffensiveCount = value;
				this.SendPropertyChanged("OffensiveCount");
				this.OnOffensiveCountChanged();
			}
		}
	}
	
	[Column(Storage="_Privacy", DbType="TinyInt NOT NULL")]
	public byte Privacy
	{
		get
		{
			return this._Privacy;
		}
		set
		{
			if ((this._Privacy != value))
			{
				this.OnPrivacyChanging(value);
				this.SendPropertyChanging();
				this._Privacy = value;
				this.SendPropertyChanged("Privacy");
				this.OnPrivacyChanged();
			}
		}
	}
	
	[Association(Name="User_Image", Storage="_User", ThisKey="UserId", OtherKey="Id", IsForeignKey=true)]
	public User User
	{
		get
		{
			return this._User.Entity;
		}
		set
		{
			User previousValue = this._User.Entity;
			if (((previousValue != value) 
						|| (this._User.HasLoadedOrAssignedValue == false)))
			{
				this.SendPropertyChanging();
				if ((previousValue != null))
				{
					this._User.Entity = null;
					previousValue.UserImages.Remove(this);
				}
				this._User.Entity = value;
				if ((value != null))
				{
					value.UserImages.Add(this);
					this._UserId = value.Id;
				}
				else
				{
					this._UserId = default(System.Guid);
				}
				this.SendPropertyChanged("User");
			}
		}
	}
	
	public event PropertyChangingEventHandler PropertyChanging;
	
	public event PropertyChangedEventHandler PropertyChanged;
	
	protected virtual void SendPropertyChanging()
	{
		if ((this.PropertyChanging != null))
		{
			this.PropertyChanging(this, emptyChangingEventArgs);
		}
	}
	
	protected virtual void SendPropertyChanged(String propertyName)
	{
		if ((this.PropertyChanged != null))
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
#pragma warning restore 1591

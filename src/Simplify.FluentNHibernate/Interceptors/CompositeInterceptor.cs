using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;

#nullable disable

namespace Simplify.FluentNHibernate.Interceptors
{
	/// <summary>
	/// Provides functionality of using multiple interceptors
	/// </summary>
	public class CompositeInterceptor : IInterceptor
	{
		private readonly IInterceptor[] _interceptors;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeInterceptor"/> class.
		/// </summary>
		/// <param name="interceptors">List of interceptors</param>
		/// <exception cref="ArgumentNullException">At least one interceptor must be set</exception>
		public CompositeInterceptor(params IInterceptor[] interceptors)
		{
			if (interceptors is not { Length: > 0 })
				throw new ArgumentNullException(nameof(interceptors), "At least one interceptor must be set");

			_interceptors = interceptors;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeInterceptor"/> class.
		/// </summary>
		/// <param name="interceptors">List of interceptors</param>
		/// <exception cref="ArgumentNullException">At least one interceptor must be set</exception>
		public CompositeInterceptor(IEnumerable<IInterceptor> interceptors)
			: this(interceptors?.ToArray())
		{
		}

		/// <summary>
		/// Called when a NHibernate transaction is begun via the NHibernate NHibernate.ITransaction
		/// API. Will not be called if transactions are being controlled via some other mechanism.
		/// </summary>
		/// <param name="tx">The transaction</param>
		public void AfterTransactionBegin(ITransaction tx)
		{
			foreach (var i in _interceptors)
				i.AfterTransactionBegin(tx);
		}

		/// <summary>
		///  Called after a transaction is committed or rolled back.
		/// </summary>
		/// <param name="tx">The transaction</param>
		public void AfterTransactionCompletion(ITransaction tx)
		{
			foreach (var i in _interceptors)
				i.AfterTransactionCompletion(tx);
		}

		/// <summary>
		/// Called before a transaction is committed (but not before rollback).
		/// </summary>
		/// <param name="tx">The transaction</param>
		public void BeforeTransactionCompletion(ITransaction tx)
		{
			foreach (var i in _interceptors)
				i.BeforeTransactionCompletion(tx);
		}

		/// <summary>
		/// Called from Flush(). The return value determines whether the entity is updated
		/// </summary>
		/// <param name="entity">A persistent entity</param>
		/// <param name="id">Object identifier</param>
		/// <param name="currentState">The current state</param>
		/// <param name="previousState">The previous state</param>
		/// <param name="propertyNames">The property names</param>
		/// <param name="types">The types</param>
		/// <returns>An array of dirty property indicies or null to choose default behavior</returns>
		public int[] FindDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types) =>
			_interceptors
				.Select(i => i.FindDirty(entity, id, currentState, previousState, propertyNames, types))
				.Where(result => result is not null)
				.SelectMany(x => x)
				.Distinct()
				.ToArray();

		/// <summary>
		/// Get a fully loaded entity instance that is cached externally
		/// </summary>
		/// <param name="entityName">The name of the entity</param>
		/// <param name="id">The instance identifier</param>
		public object GetEntity(string entityName, object id) =>
			_interceptors
				.Select(i => i.GetEntity(entityName, id))
				.FirstOrDefault(result => result is not null);

		/// <summary>
		/// Get the entity name for a persistent or transient instance
		/// </summary>
		/// <param name="entity">An entity instance</param>
		/// <returns></returns>
		public string GetEntityName(object entity) =>
			_interceptors
				.Select(i => i.GetEntityName(entity))
				.FirstOrDefault(result => result is not null);

		/// <summary>
		/// Instantiate the entity class. Return null to indicate that Hibernate should use
		/// the default constructor of the class
		/// </summary>
		/// <param name="entityName">The name of the entity</param>
		/// <param name="id">The identifier of the new instance</param>
		/// <returns>An instance of the class, or null to choose default behavior</returns>
		public object Instantiate(string entityName, object id) =>
			_interceptors
				.Select(interceptor => interceptor.Instantiate(entityName, id))
				.FirstOrDefault(result => result is not null);

		/// <summary>
		/// Called when a transient entity is passed to SaveOrUpdate.
		/// </summary>
		/// <param name="entity">A transient entity</param>
		/// <returns> Boolean or null to choose default behavior</returns>
		public bool? IsTransient(object entity) =>
			_interceptors
				.Select(i => i.IsTransient(entity))
				.FirstOrDefault(result => result is not null);

		/// <summary>
		/// Called before a collection is (re)created.
		/// </summary>
		/// <param name="collection">The collection</param>
		/// <param name="key">The key</param>
		public void OnCollectionRecreate(object collection, object key)
		{
			foreach (var i in _interceptors)
				i.OnCollectionRecreate(collection, key);
		}

		/// <summary>
		/// Called before a collection is deleted.
		/// </summary>
		/// <param name="collection">The collection</param>
		/// <param name="key">The key</param>
		public void OnCollectionRemove(object collection, object key)
		{
			foreach (var i in _interceptors)
				i.OnCollectionRemove(collection, key);
		}

		/// <summary>
		/// Called before a collection is updated.
		/// </summary>
		/// <param name="collection">The collection</param>
		/// <param name="key">The key</param>
		public void OnCollectionUpdate(object collection, object key)
		{
			foreach (var i in _interceptors)
				i.OnCollectionUpdate(collection, key);
		}

		/// <summary>
		/// Called before an object is deleted
		/// </summary>
		/// <param name="entity">A persistent entity</param>
		/// <param name="id">Object identifier</param>
		/// <param name="state">The state</param>
		/// <param name="propertyNames">The property names</param>
		/// <param name="types">The types</param>
		public void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
		{
			foreach (var i in _interceptors)
				i.OnDelete(entity, id, state, propertyNames, types);
		}

		/// <summary>
		/// Called when an object is detected to be dirty, during a flush.
		/// </summary>
		/// <param name="entity">A persistent entity</param>
		/// <param name="id">Object identifier</param>
		/// <param name="currentState">The current state</param>
		/// <param name="previousState">The previous state</param>
		/// <param name="propertyNames">The property names</param>
		/// <param name="types">The types</param>
		/// <returns>True if the user modified the currentState in any way</returns>
		public bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types) =>
			_interceptors
				.Any(i => i.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types));

		/// <summary>
		/// Called just before an object is initialized
		/// </summary>
		/// <param name="entity">A persistent entity</param>
		/// <param name="id">Object identifier</param>
		/// <param name="state">The state</param>
		/// <param name="propertyNames">The property names</param>
		/// <param name="types">The types</param>
		/// <returns>True if the user modified the state in any way</returns>
		public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types) =>
			_interceptors
				.Any(i => i.OnLoad(entity, id, state, propertyNames, types));

		/// <summary>
		/// Called when sql string is being prepared.
		/// </summary>
		/// <param name="sql">The sql</param>
		/// <returns>Original or modified sql</returns>
		public SqlString OnPrepareStatement(SqlString sql) =>
			_interceptors.
				Aggregate(sql, (current, i) => i.OnPrepareStatement(current));

		/// <summary>
		/// Called before an object is saved
		/// </summary>
		/// <param name="entity">A persistent entity</param>
		/// <param name="id">Object identifier</param>
		/// <param name="state">The state</param>
		/// <param name="propertyNames">The property names</param>
		/// <param name="types">The types</param>
		/// <returns>True if the user modified the state in any way</returns>
		public bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types) =>
			_interceptors
				.Any(i => i.OnSave(entity, id, state, propertyNames, types));

		/// <summary>
		/// Called after a flush that actually ends in execution of the SQL statements required
		/// to synchronize in-memory state with the database.
		/// </summary>
		/// <param name="entities">The entities</param>
		public void PostFlush(ICollection entities)
		{
			foreach (var i in _interceptors)
				i.PostFlush(entities);
		}

		/// <summary>
		/// Called before a flush
		/// </summary>
		/// <param name="entities">The entities</param>
		public void PreFlush(ICollection entities)
		{
			foreach (var i in _interceptors)
				i.PreFlush(entities);
		}

		/// <summary>
		/// Called when a session-scoped (and only session scoped) interceptor is attached
		/// to a session
		/// </summary>
		/// <param name="session">The session</param>
		public void SetSession(ISession session)
		{
			foreach (var i in _interceptors)
				i.SetSession(session);
		}
	}
}
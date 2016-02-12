#region Using Namespaces...

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Entity.Validation;
using SuiviPortefeuilleRBC.Models;

#endregion

namespace SuiviPortefeuilleRBC.Repository
{
   /// <summary>
   /// Unit of Work class responsible for DB transactions
   /// </summary>
   public class UnitOfWork : IDisposable
   {
      #region Private member variables...

      private ApplicationDbContext context = null;
      private GenericRepository<Stock> stockRepository;
      private GenericRepository<StockDescription> stockDescriptionRepository;
      private GenericRepository<Portfolio> portfolioRepository;
      private GenericRepository<Operation> operationRepository;

      #endregion

      public UnitOfWork()
      {
         context = new ApplicationDbContext();
      }

      #region Public Repository Creation properties...

      /// <summary>
      /// Get/Set Property for StockDescription repository.
      /// </summary>
      public GenericRepository<StockDescription> StockDescriptionRepository
      {
         get
         {
            if(this.stockDescriptionRepository == null)
               this.stockDescriptionRepository = new GenericRepository<StockDescription>(context);
            return stockDescriptionRepository;
         }
      }

      /// <summary>
      /// Get/Set Property for Stock repository.
      /// </summary>
      public GenericRepository<Stock> StockRepository
      {
         get
         {
            if(this.stockRepository == null)
               this.stockRepository = new GenericRepository<Stock>(context);
            return stockRepository;
         }
      }

      /// <summary>
      /// Get/Set Property for Portfolio repository.
      /// </summary>
      public GenericRepository<Portfolio> PortfolioRepository
      {
         get
         {
            if(this.portfolioRepository == null)
               this.portfolioRepository = new GenericRepository<Portfolio>(context);
            return portfolioRepository;
         }
      }

      /// <summary>
      /// Get/Set Property for Operation repository.
      /// </summary>
      public GenericRepository<Operation> OperationRepository
      {
         get
         {
            if(this.operationRepository == null)
               this.operationRepository = new GenericRepository<Operation>(context);
            return operationRepository;
         }
      }

      #endregion

      #region Methods

      /// <summary>
      /// Save method.
      /// </summary>
      public void Save()
      {
         try
         {
            context.SaveChanges();
         }
         catch(DbEntityValidationException e)
         {

            var outputLines = new List<string>();
            foreach(var eve in e.EntityValidationErrors)
            {
               outputLines.Add(string.Format(
                   "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                   eve.Entry.Entity.GetType().Name, eve.Entry.State));
               foreach(var ve in eve.ValidationErrors)
               {
                  outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
               }
            }
            System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

            throw e;
         }

      }

      #endregion

      #region IDiosposable

      #region private dispose variable declaration

      private bool disposed = false;

      #endregion

      /// <summary>
      /// Protected Virtual Dispose method
      /// </summary>
      /// <param name="disposing"></param>
      protected virtual void Dispose(bool disposing)
      {
         if(!this.disposed)
         {
            if(disposing)
            {
               Debug.WriteLine("UnitOfWork is being disposed");
               context.Dispose();
            }
         }
         this.disposed = true;
      }

      /// <summary>
      /// Dispose method
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }
      #endregion
   }
}
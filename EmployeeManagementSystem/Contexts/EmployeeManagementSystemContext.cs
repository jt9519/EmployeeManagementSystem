using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.DataModel;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace EmployeeManagementSystem.Contexts
{
    public class EmployeeManagementSystemContext : DbContext
    {
        public DbSet<EmployeeModel> Employee { get; set; } // Employee テーブル
        public DbSet<OperatorModel> Operator { get; set; } // Operation テーブル
        public DbSet<PositionModel> Position { get; set; } // Position テーブル
        public DbSet<OfficeModel> Office { get; set; } // Office テーブル


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // OfficeModelの設定（スキーマ名: public, テーブル名: office）
            modelBuilder.Entity<OfficeModel>()
                .ToTable("office", "public")
                .HasKey(o => o.office_id); // Officeテーブルの主キー

            // PositionModelの設定（スキーマ名: public, テーブル名: position）
            modelBuilder.Entity<PositionModel>()
                .ToTable("position", "public")
                .HasKey(p => p.position_id); // Positionテーブルの主キー

            // OperatorModelの設定（スキーマ名: public, テーブル名: operator）
            modelBuilder.Entity<OperatorModel>()
                .ToTable("operator", "public")
                .HasKey(op => op.operator_id); // Operatorテーブルの主キー
            modelBuilder.Entity<OperatorModel>()
                .HasOne(op => op.EmployeeIdNavigation) // 外部キーとしてEmployeeIdを指定
                .WithMany(e => e.Operators)
                .HasForeignKey(op => op.employee_id); // OperatorModelとEmployeeModelのリレーション

            // EmployeeModelの設定（スキーマ名: public, テーブル名: employee）
            modelBuilder.Entity<EmployeeModel>()
                .ToTable("employee", "public");

        }

        private readonly StringBuilder _logBuilder = new StringBuilder(); // ログ蓄積用


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=127.0.0.1;Port=5432;Username=postgres;Password=Itigo332@@;Database=EmployeeManagementSystem")
                .LogTo(log => _logBuilder.AppendLine(log), LogLevel.Information)  // ログ出力
                .EnableSensitiveDataLogging(); // パラメーター値を出力する
        }

        // ログを取得するためのメソッド
        public string GetLogs()
        {
            return _logBuilder.ToString();
        }

    }
}

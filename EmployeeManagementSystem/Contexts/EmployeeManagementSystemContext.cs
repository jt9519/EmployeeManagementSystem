using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagementSystem.DataModel;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;
using System.Configuration;
using Pomelo.EntityFrameworkCore.MySql;




namespace EmployeeManagementSystem.Contexts
{
    public class EmployeeManagementSystemContext : DbContext
    {
        public DbSet<EmployeeModel> Employee { get; set; } // Employee テーブル
        public DbSet<OperatorModel> Operator { get; set; } // Operation テーブル
        public DbSet<PositionModel> Position { get; set; } // Position テーブル
        public DbSet<OfficeModel> Office { get; set; } // Office テーブル
        public DbSet<SessionModel> Session { get; set; } //Session テーブル
        public DbSet<EmployeeView> EmployeeView { get; set; }



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
                .WithMany(e => e.Operator)
                .HasForeignKey(op => op.employee_id); // OperatorModelとEmployeeModelのリレーション

            // EmployeeModelの設定（スキーマ名: public, テーブル名: employee）
            modelBuilder.Entity<EmployeeModel>()
                .ToTable("employee", "public");



            // EmployeeView をデータベースビューとして設定
            modelBuilder.Entity<EmployeeView>().ToView("EmployeeView"); // ビュー名を指定

            // EmployeeView のプライマリーキーを設定
            modelBuilder.Entity<EmployeeView>().HasKey(e => e.employee_id); // 正確なプロパティ名を使用

            // SessionModelの設定（スキーマ名: public, テーブル名: session）
            modelBuilder.Entity<SessionModel>()
                .ToTable("session", "public")
                .HasKey(s => s.session_id); // Sessionテーブルの主キー
            modelBuilder.Entity<SessionModel>()
                .HasOne(s => s.EmployeeIdNavigation) // 外部キーとしてEmployeeIdを指定
                .WithMany(e => e.Session)
                .HasForeignKey(s => s.employee_id); // SessionModelとEmployeeModelのリレーション

            // 親クラスの設定を呼び出し
            base.OnModelCreating(modelBuilder);



        }

        private readonly StringBuilder _logBuilder = new StringBuilder(); // ログ蓄積用

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // App.configの接続文字列を利用
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 41)));

            //実行されたSQLをメッセージボックスに表示
            //optionsBuilder.LogTo(sql => MessageBox.Show($"SQLクエリ: {sql}"), LogLevel.Information);

        }

        // ログを取得するためのメソッド
        public string GetLogs()
        {
            return _logBuilder.ToString();
        }

    }
}

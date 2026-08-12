using System;
using System.Collections.Generic;
using Path = DoxygenSqliteInspector.Models.Path;
using DoxygenSqliteInspector.Models;
using Microsoft.EntityFrameworkCore;

namespace DoxygenSqliteInspector.Data;

public partial class DoxygenContext : DbContext
{
    private readonly string _dbPath;

    public DoxygenContext(string dbPath)
    {
        _dbPath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
    }

    public DoxygenContext(DbContextOptions<DoxygenContext> options)
        : base(options)
    {
        throw new InvalidOperationException("DoxygenContext requires a database file path. Use DoxygenContext(string dbPath).");
    }

    public virtual DbSet<ArgumentXref> ArgumentXrefs { get; set; }

    public virtual DbSet<Compounddef> Compounddefs { get; set; }

    public virtual DbSet<Compoundref> Compoundrefs { get; set; }

    public virtual DbSet<Contain> Contains { get; set; }

    public virtual DbSet<Def> Defs { get; set; }

    public virtual DbSet<ExternalFile> ExternalFiles { get; set; }

    public virtual DbSet<Include> Includes { get; set; }

    public virtual DbSet<InitializerXref> InitializerXrefs { get; set; }

    public virtual DbSet<InlineXref> InlineXrefs { get; set; }

    public virtual DbSet<InnerOuter> InnerOuters { get; set; }

    public virtual DbSet<LocalFile> LocalFiles { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Memberdef> Memberdefs { get; set; }

    public virtual DbSet<MemberdefParam> MemberdefParams { get; set; }

    public virtual DbSet<Metum> Meta { get; set; }

    public virtual DbSet<Param> Params { get; set; }

    public virtual DbSet<Path> Paths { get; set; }

    public virtual DbSet<Refid> Refids { get; set; }

    public virtual DbSet<Reimplement> Reimplements { get; set; }

    public virtual DbSet<Rel> Rels { get; set; }

    public virtual DbSet<Xref> Xrefs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={_dbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArgumentXref>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("argument_xrefs");

            entity.Property(e => e.DstRowid).HasColumnName("dst_rowid");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.SrcRowid).HasColumnName("src_rowid");
        });

        modelBuilder.Entity<Compounddef>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("compounddef");

            entity.Property(e => e.Rowid)
                .ValueGeneratedNever()
                .HasColumnName("rowid");
            entity.Property(e => e.Briefdescription).HasColumnName("briefdescription");
            entity.Property(e => e.Column).HasColumnName("column");
            entity.Property(e => e.Detaileddescription).HasColumnName("detaileddescription");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.HeaderId).HasColumnName("header_id");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Line).HasColumnName("line");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Prot).HasColumnName("prot");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.File).WithMany(p => p.CompounddefFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Header).WithMany(p => p.CompounddefHeaders).HasForeignKey(d => d.HeaderId);

            entity.HasOne(d => d.Row).WithOne(p => p.Compounddef)
                .HasForeignKey<Compounddef>(d => d.Rowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Compoundref>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("compoundref");

            entity.HasIndex(e => new { e.BaseRowid, e.DerivedRowid }, "IX_compoundref_base_rowid_derived_rowid").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.BaseRowid).HasColumnName("base_rowid");
            entity.Property(e => e.DerivedRowid).HasColumnName("derived_rowid");
            entity.Property(e => e.Prot).HasColumnName("prot");
            entity.Property(e => e.Virt).HasColumnName("virt");

            entity.HasOne(d => d.BaseRow).WithMany(p => p.CompoundrefBaseRows)
                .HasForeignKey(d => d.BaseRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DerivedRow).WithMany(p => p.CompoundrefDerivedRows)
                .HasForeignKey(d => d.DerivedRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Contain>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("contains");

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.InnerRowid).HasColumnName("inner_rowid");
            entity.Property(e => e.OuterRowid).HasColumnName("outer_rowid");

            entity.HasOne(d => d.InnerRow).WithMany(p => p.ContainInnerRows)
                .HasForeignKey(d => d.InnerRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.OuterRow).WithMany(p => p.ContainOuterRows)
                .HasForeignKey(d => d.OuterRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Def>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("def");

            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Refid).HasColumnName("refid");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Summary).HasColumnName("summary");
        });

        modelBuilder.Entity<ExternalFile>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("external_file");

            entity.Property(e => e.Found).HasColumnName("found");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
        });

        modelBuilder.Entity<Include>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("includes");

            entity.HasIndex(e => new { e.Local, e.SrcId, e.DstId }, "IX_includes_local_src_id_dst_id").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.DstId).HasColumnName("dst_id");
            entity.Property(e => e.Local).HasColumnName("local");
            entity.Property(e => e.SrcId).HasColumnName("src_id");

            entity.HasOne(d => d.Dst).WithMany(p => p.IncludeDsts)
                .HasForeignKey(d => d.DstId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Src).WithMany(p => p.IncludeSrcs)
                .HasForeignKey(d => d.SrcId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<InitializerXref>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("initializer_xrefs");

            entity.Property(e => e.DstRowid).HasColumnName("dst_rowid");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.SrcRowid).HasColumnName("src_rowid");
        });

        modelBuilder.Entity<InlineXref>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("inline_xrefs");

            entity.Property(e => e.DstRowid).HasColumnName("dst_rowid");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.SrcRowid).HasColumnName("src_rowid");
        });

        modelBuilder.Entity<InnerOuter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("inner_outer");

            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Kind1).HasColumnName("kind:1");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Name1).HasColumnName("name:1");
            entity.Property(e => e.Refid).HasColumnName("refid");
            entity.Property(e => e.Refid1).HasColumnName("refid:1");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Rowid1).HasColumnName("rowid:1");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Summary1).HasColumnName("summary:1");
        });

        modelBuilder.Entity<LocalFile>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("local_file");

            entity.Property(e => e.Found).HasColumnName("found");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("member");

            entity.HasIndex(e => new { e.ScopeRowid, e.MemberdefRowid }, "IX_member_scope_rowid_memberdef_rowid").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.MemberdefRowid).HasColumnName("memberdef_rowid");
            entity.Property(e => e.Prot).HasColumnName("prot");
            entity.Property(e => e.ScopeRowid).HasColumnName("scope_rowid");
            entity.Property(e => e.Virt).HasColumnName("virt");

            entity.HasOne(d => d.MemberdefRow).WithMany(p => p.Members)
                .HasForeignKey(d => d.MemberdefRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ScopeRow).WithMany(p => p.Members)
                .HasForeignKey(d => d.ScopeRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Memberdef>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("memberdef");

            entity.Property(e => e.Rowid)
                .ValueGeneratedNever()
                .HasColumnName("rowid");
            entity.Property(e => e.Accessor)
                .HasDefaultValue(0)
                .HasColumnName("accessor");
            entity.Property(e => e.Addable)
                .HasDefaultValue(0)
                .HasColumnName("addable");
            entity.Property(e => e.Argsstring).HasColumnName("argsstring");
            entity.Property(e => e.Attribute)
                .HasDefaultValue(0)
                .HasColumnName("attribute");
            entity.Property(e => e.Bitfield).HasColumnName("bitfield");
            entity.Property(e => e.Bodyend)
                .HasDefaultValue(0)
                .HasColumnName("bodyend");
            entity.Property(e => e.BodyfileId).HasColumnName("bodyfile_id");
            entity.Property(e => e.Bodystart)
                .HasDefaultValue(0)
                .HasColumnName("bodystart");
            entity.Property(e => e.Bound)
                .HasDefaultValue(0)
                .HasColumnName("bound");
            entity.Property(e => e.Briefdescription).HasColumnName("briefdescription");
            entity.Property(e => e.Column).HasColumnName("column");
            entity.Property(e => e.Const)
                .HasDefaultValue(0)
                .HasColumnName("const");
            entity.Property(e => e.Constrained)
                .HasDefaultValue(0)
                .HasColumnName("constrained");
            entity.Property(e => e.Definition).HasColumnName("definition");
            entity.Property(e => e.Detaileddescription).HasColumnName("detaileddescription");
            entity.Property(e => e.Explicit)
                .HasDefaultValue(0)
                .HasColumnName("explicit");
            entity.Property(e => e.Extern)
                .HasDefaultValue(0)
                .HasColumnName("extern");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Final)
                .HasDefaultValue(0)
                .HasColumnName("final");
            entity.Property(e => e.Gettable)
                .HasDefaultValue(0)
                .HasColumnName("gettable");
            entity.Property(e => e.Inbodydescription).HasColumnName("inbodydescription");
            entity.Property(e => e.Initializer).HasColumnName("initializer");
            entity.Property(e => e.Initonly)
                .HasDefaultValue(0)
                .HasColumnName("initonly");
            entity.Property(e => e.Inline)
                .HasDefaultValue(0)
                .HasColumnName("inline");
            entity.Property(e => e.Kind).HasColumnName("kind");
            entity.Property(e => e.Line).HasColumnName("line");
            entity.Property(e => e.Maybeambiguous)
                .HasDefaultValue(0)
                .HasColumnName("maybeambiguous");
            entity.Property(e => e.Maybedefault)
                .HasDefaultValue(0)
                .HasColumnName("maybedefault");
            entity.Property(e => e.Maybevoid)
                .HasDefaultValue(0)
                .HasColumnName("maybevoid");
            entity.Property(e => e.Mutable)
                .HasDefaultValue(0)
                .HasColumnName("mutable");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.New)
                .HasDefaultValue(0)
                .HasColumnName("new");
            entity.Property(e => e.Optional)
                .HasDefaultValue(0)
                .HasColumnName("optional");
            entity.Property(e => e.Privategettable)
                .HasDefaultValue(0)
                .HasColumnName("privategettable");
            entity.Property(e => e.Privatesettable)
                .HasDefaultValue(0)
                .HasColumnName("privatesettable");
            entity.Property(e => e.Property)
                .HasDefaultValue(0)
                .HasColumnName("property");
            entity.Property(e => e.Prot)
                .HasDefaultValue(0)
                .HasColumnName("prot");
            entity.Property(e => e.Protectedgettable)
                .HasDefaultValue(0)
                .HasColumnName("protectedgettable");
            entity.Property(e => e.Protectedsettable)
                .HasDefaultValue(0)
                .HasColumnName("protectedsettable");
            entity.Property(e => e.Raisable)
                .HasDefaultValue(0)
                .HasColumnName("raisable");
            entity.Property(e => e.Read).HasColumnName("read");
            entity.Property(e => e.Readable)
                .HasDefaultValue(0)
                .HasColumnName("readable");
            entity.Property(e => e.Readonly)
                .HasDefaultValue(0)
                .HasColumnName("readonly");
            entity.Property(e => e.Removable)
                .HasDefaultValue(0)
                .HasColumnName("removable");
            entity.Property(e => e.Required)
                .HasDefaultValue(0)
                .HasColumnName("required");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.Sealed)
                .HasDefaultValue(0)
                .HasColumnName("sealed");
            entity.Property(e => e.Settable)
                .HasDefaultValue(0)
                .HasColumnName("settable");
            entity.Property(e => e.Static)
                .HasDefaultValue(0)
                .HasColumnName("static");
            entity.Property(e => e.Transient)
                .HasDefaultValue(0)
                .HasColumnName("transient");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Virt)
                .HasDefaultValue(0)
                .HasColumnName("virt");
            entity.Property(e => e.Volatile)
                .HasDefaultValue(0)
                .HasColumnName("volatile");
            entity.Property(e => e.Writable)
                .HasDefaultValue(0)
                .HasColumnName("writable");
            entity.Property(e => e.Write).HasColumnName("write");

            entity.HasOne(d => d.Bodyfile).WithMany(p => p.MemberdefBodyfiles).HasForeignKey(d => d.BodyfileId);

            entity.HasOne(d => d.File).WithMany(p => p.MemberdefFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Row).WithOne(p => p.Memberdef)
                .HasForeignKey<Memberdef>(d => d.Rowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<MemberdefParam>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("memberdef_param");

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.MemberdefId).HasColumnName("memberdef_id");
            entity.Property(e => e.ParamId).HasColumnName("param_id");

            entity.HasOne(d => d.Memberdef).WithMany(p => p.MemberdefParams)
                .HasForeignKey(d => d.MemberdefId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Param).WithMany(p => p.MemberdefParams)
                .HasForeignKey(d => d.ParamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Metum>(entity =>
        {
            entity.HasKey(e => e.DoxygenVersion);

            entity.ToTable("meta");

            entity.Property(e => e.DoxygenVersion).HasColumnName("doxygen_version");
            entity.Property(e => e.GeneratedAt).HasColumnName("generated_at");
            entity.Property(e => e.GeneratedOn).HasColumnName("generated_on");
            entity.Property(e => e.ProjectBrief).HasColumnName("project_brief");
            entity.Property(e => e.ProjectName).HasColumnName("project_name");
            entity.Property(e => e.ProjectNumber).HasColumnName("project_number");
            entity.Property(e => e.SchemaVersion).HasColumnName("schema_version");
        });

        modelBuilder.Entity<Param>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("param");

            entity.HasIndex(e => new { e.Type, e.Defname }, "idx_param").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Array).HasColumnName("array");
            entity.Property(e => e.Attributes).HasColumnName("attributes");
            entity.Property(e => e.Briefdescription).HasColumnName("briefdescription");
            entity.Property(e => e.Declname).HasColumnName("declname");
            entity.Property(e => e.Defname).HasColumnName("defname");
            entity.Property(e => e.Defval).HasColumnName("defval");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Path>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("path");

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Found).HasColumnName("found");
            entity.Property(e => e.Local).HasColumnName("local");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Refid>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("refid");

            entity.HasIndex(e => e.Refid1, "IX_refid_refid").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Refid1).HasColumnName("refid");
        });

        modelBuilder.Entity<Reimplement>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("reimplements");

            entity.HasIndex(e => new { e.MemberdefRowid, e.ReimplementedRowid }, "IX_reimplements_memberdef_rowid_reimplemented_rowid").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.MemberdefRowid).HasColumnName("memberdef_rowid");
            entity.Property(e => e.ReimplementedRowid).HasColumnName("reimplemented_rowid");

            entity.HasOne(d => d.MemberdefRow).WithMany(p => p.ReimplementMemberdefRows)
                .HasForeignKey(d => d.MemberdefRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ReimplementedRow).WithMany(p => p.ReimplementReimplementedRows)
                .HasForeignKey(d => d.ReimplementedRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Rel>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("rel");

            entity.Property(e => e.ArgumentLinksIn).HasColumnName("argument_links_in");
            entity.Property(e => e.ArgumentLinksOut).HasColumnName("argument_links_out");
            entity.Property(e => e.Compounds).HasColumnName("compounds");
            entity.Property(e => e.InitializerLinksIn).HasColumnName("initializer_links_in");
            entity.Property(e => e.InitializerLinksOut).HasColumnName("initializer_links_out");
            entity.Property(e => e.Innerclasses).HasColumnName("innerclasses");
            entity.Property(e => e.Innercompounds).HasColumnName("innercompounds");
            entity.Property(e => e.Innerdirs).HasColumnName("innerdirs");
            entity.Property(e => e.Innerfiles).HasColumnName("innerfiles");
            entity.Property(e => e.Innergroups).HasColumnName("innergroups");
            entity.Property(e => e.Innernamespaces).HasColumnName("innernamespaces");
            entity.Property(e => e.Innerpages).HasColumnName("innerpages");
            entity.Property(e => e.LinksIn).HasColumnName("links_in");
            entity.Property(e => e.LinksOut).HasColumnName("links_out");
            entity.Property(e => e.Members).HasColumnName("members");
            entity.Property(e => e.Outerclasses).HasColumnName("outerclasses");
            entity.Property(e => e.Outercompounds).HasColumnName("outercompounds");
            entity.Property(e => e.Outerdirs).HasColumnName("outerdirs");
            entity.Property(e => e.Outerfiles).HasColumnName("outerfiles");
            entity.Property(e => e.Outergroups).HasColumnName("outergroups");
            entity.Property(e => e.Outernamespaces).HasColumnName("outernamespaces");
            entity.Property(e => e.Outerpages).HasColumnName("outerpages");
            entity.Property(e => e.Reimplemented).HasColumnName("reimplemented");
            entity.Property(e => e.Reimplements).HasColumnName("reimplements");
            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Subclasses).HasColumnName("subclasses");
            entity.Property(e => e.Superclasses).HasColumnName("superclasses");
        });

        modelBuilder.Entity<Xref>(entity =>
        {
            entity.HasKey(e => e.Rowid);

            entity.ToTable("xrefs");

            entity.HasIndex(e => new { e.SrcRowid, e.DstRowid, e.Context }, "IX_xrefs_src_rowid_dst_rowid_context").IsUnique();

            entity.Property(e => e.Rowid).HasColumnName("rowid");
            entity.Property(e => e.Context).HasColumnName("context");
            entity.Property(e => e.DstRowid).HasColumnName("dst_rowid");
            entity.Property(e => e.SrcRowid).HasColumnName("src_rowid");

            entity.HasOne(d => d.DstRow).WithMany(p => p.XrefDstRows)
                .HasForeignKey(d => d.DstRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SrcRow).WithMany(p => p.XrefSrcRows)
                .HasForeignKey(d => d.SrcRowid)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

RAW_API=gtkspell-api.raw
API=gtkspell-api.xml
METADATA=gtkspell.metadata

ASSEMBLY_NAME= gtkspell-sharp
ASSEMBLY= $(ASSEMBLY_NAME).dll

if WINBUILD
CODEGEN=gapi_codegen.exe
FIXUP=gapi-fixup.exe
else
CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup
endif

sources = 

build_sources = $(addprefix $(srcdir)/, $(sources)) AssemblyInfo.cs

customs = 

CLEANFILES = gtkspell-sharp.dll generated-stamp generated/*.cs $(API) gtkspell-sharp.snk

DISTCLEANFILES= AssemblyInfo.cs

noinst_DATA = gtkspell-sharp.dll

all: generated-stamp gtkspell-sharp.dll

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $(API)
	chmod u+w $(API)
	$(FIXUP) --api=$(srcdir)/$(API) --metadata=$(srcdir)/$(METADATA)

generated-stamp: $(API)
	 $(CODEGEN) --generate $(srcdir)/$(API) $(GTK_SHARP_CFLAGS) --outdir=generated --customdir=$(srcdir) --assembly-name=gtkspell-sharp && touch generated-stamp

gtkspell-sharp.snk: $(top_srcdir)/gtkspell-sharp.snk
	cp $(top_srcdir)/gtkspell-sharp.snk .

gtkspell-sharp.dll: $(build_sources) generated-stamp gtkspell-sharp.snk
	$(CSC) -out:gtkspell-sharp.dll -unsafe -target:library $(GTK_SHARP_LIBS) \
	$(build_sources) $(GENERATED_SOURCES)

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY) /f /package gtkspell-sharp-2.0 /root $(DESTDIR)$(libdir)";  \
        $(GACUTIL) /i $(ASSEMBLY) /f /package gtkspell-sharp-2.0 /root $(DESTDIR)$(libdir) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) /package gtkspell-sharp-2.0 /root $(DESTDIR)$(libdir)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) /package gtkspell-sharp-2.0 /root $(DESTDIR)$(libdir) || exit 1;

EXTRA_DIST = $(RAW_API) $(sources) $(customs) $(METADATA) AssemblyInfo.cs.in gtkspell-sharp.dll.config.in

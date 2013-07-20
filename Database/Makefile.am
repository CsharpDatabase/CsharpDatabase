
EXTRA_DIST =  

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG"
ASSEMBLY = ../Bin/Debug/CsharpDatabase.dll
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../External/CsharpSQLite/Run/Debug/System.Data.SQLite.dll
BUILD_DIR = ../Bin/Debug

CSHARPDATABASE_DLL_MDB_SOURCE=../Bin/Debug/CsharpDatabase.dll.mdb
CSHARPDATABASE_DLL_MDB=$(BUILD_DIR)/CsharpDatabase.dll.mdb
MYSQL_DATA_DLL_SOURCE=../Dependencies/MySql.Data.dll
SYSTEM_DATA_SQLITE_DLL_SOURCE=../External/CsharpSQLite/Run/Debug/System.Data.SQLite.dll
SYSTEM_DATA_SQLITE_DLL_MDB_SOURCE=../External/CsharpSQLite/Run/Debug/System.Data.SQLite.dll.mdb
SYSTEM_DATA_SQLITE_DLL_MDB=$(BUILD_DIR)/System.Data.SQLite.dll.mdb

endif

if ENABLE_RELEASE
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+ "-define:RELEASE"
ASSEMBLY = ../Bin/Release/CsharpDatabase.dll
ASSEMBLY_MDB = 
COMPILE_TARGET = library
PROJECT_REFERENCES =  \
	../External/CsharpSQLite/Run/Release/System.Data.SQLite.dll
BUILD_DIR = ../Bin/Release

CSHARPDATABASE_DLL_MDB=
MYSQL_DATA_DLL_SOURCE=../Dependencies/MySql.Data.dll
SYSTEM_DATA_SQLITE_DLL_SOURCE=../External/CsharpSQLite/Run/Release/System.Data.SQLite.dll
SYSTEM_DATA_SQLITE_DLL_MDB=

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(CSHARPDATABASE_DLL_MDB) \
	$(MYSQL_DATA_DLL) \
	$(SYSTEM_DATA_SQLITE_DLL) \
	$(SYSTEM_DATA_SQLITE_DLL_MDB)  

LINUX_PKGCONFIG = \
	$(CSHARPDATABASE_PC)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

FILES = \
	Properties/AssemblyInfo.cs \
	MySql.cs \
	SQLite.cs \
	Database.cs \
	Localization/LocalizationConsole.cs \
	Singleton.cs \
	Extensions/GeneralExtensions.cs \
	Exceptions/SingletonException.cs \
	Exceptions/CDatabaseException.cs \
	DatabaseType.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	csharpdatabase.pc.in 

REFERENCES =  \
	System \
	System.Data \
	System.Xml

DLL_REFERENCES =  \
	../Dependencies/MySql.Data.dll

CLEANFILES = $(PROGRAMFILES) $(LINUX_PKGCONFIG) 

include $(top_srcdir)/Makefile.include

MYSQL_DATA_DLL = $(BUILD_DIR)/MySql.Data.dll
SYSTEM_DATA_SQLITE_DLL = $(BUILD_DIR)/System.Data.SQLite.dll
CSHARPDATABASE_PC = $(BUILD_DIR)/csharpdatabase.pc

$(eval $(call emit-deploy-target,MYSQL_DATA_DLL))
$(eval $(call emit-deploy-target,SYSTEM_DATA_SQLITE_DLL))
$(eval $(call emit-deploy-target,SYSTEM_DATA_SQLITE_DLL_MDB))
$(eval $(call emit-deploy-wrapper,CSHARPDATABASE_PC,csharpdatabase.pc))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)

# .NET 10.0 Upgrade Plan

## Table of Contents

1. [Executive Summary](#executive-summary)
2. [Migration Strategy](#migration-strategy)
3. [Detailed Dependency Analysis](#detailed-dependency-analysis)
4. [Project-by-Project Plans](#project-by-project-plans)
5. [Risk Management](#risk-management)
6. [Testing & Validation Strategy](#testing--validation-strategy)
7. [Complexity & Effort Assessment](#complexity--effort-assessment)
8. [Source Control Strategy](#source-control-strategy)
9. [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Overview

This plan documents the upgrade of **ASP.NET Core in Action 3rd Edition** solution from **.NET 7.0 to .NET 10.0 (LTS)**. The solution contains sample projects organized by chapter, demonstrating various ASP.NET Core features and patterns.

### Scope

- **Total Projects**: 120 projects (124 analyzed, 1 already on .NET 10.0, 3 duplicates)
- **Current State**: All projects targeting .NET 7.0
- **Target State**: All projects targeting .NET 10.0
- **Project Types**: ASP.NET Core Web Applications, Blazor WebAssembly, Console Applications, Worker Services, Test Projects

### Assessment Summary

**Total Issues Identified**: 812 issues across 204 files
- **Mandatory**: 126 issues (primarily target framework changes, binary incompatible APIs)
- **Potential**: 681 issues (source incompatible APIs, behavioral changes, package updates)
- **Optional**: 5 issues (deprecated packages)

**Key Issue Categories**:
1. **Source Incompatible APIs** (317 occurrences): Obsolete API usage requiring code changes
2. **Behavioral Changes** (216 occurrences): Runtime behavior differences in .NET 10.0
3. **NuGet Package Updates** (148 occurrences): 23 packages need version updates
4. **Target Framework Changes** (119 occurrences): All .NET 7.0 projects
5. **Binary Incompatible APIs** (7 occurrences): 4 projects affected
6. **Deprecated Packages** (5 occurrences): FluentValidation.AspNetCore, hosting extensions

### Complexity Classification: **Complex**

**Justification**:
- **Project Count**: 120 projects exceeds the threshold for medium complexity (>15)
- **Dependency Depth**: Single level (Level 0: 117 projects, Level 1: 3 test projects)
- **No Circular Dependencies**: Clean dependency graph
- **High-Risk Projects**: 4 projects with binary incompatible APIs
- **Deprecated Packages**: 4 projects using deprecated packages (Quartz hosting, FluentValidation)
- **Technology Diversity**: Mixed project types (Web, API, Blazor, Workers, Console, Tests)

**Complexity Factors**:
- Large number of independent projects (117 at Level 0)
- Significant source incompatible API usage (317 occurrences)
- Entity Framework Core migrations across multiple projects
- Identity/Authentication projects with breaking changes
- Worker Services with deprecated dependencies

### Selected Strategy: Bottom-Up (Dependency-First)

**Rationale**:
- Solution has clear dependency hierarchy (2 levels)
- 117 independent projects with no internal dependencies
- 3 test projects depend on their respective application projects
- Can process projects in batches by complexity and risk level
- Enables systematic validation at each tier

**Strategy Application**:
- **Tier 0**: All 117 independent projects (no project dependencies)
- **Tier 1**: 3 test projects (depend only on Tier 0 projects)

Within Tier 0, projects will be batched by:
1. **Risk level**: Binary incompatible > Deprecated packages > Standard
2. **Complexity**: High complexity (EF Core, Identity, many issues) processed separately
3. **Project type**: Similar projects batched together

### Critical Issues Requiring Attention

**Binary Incompatible APIs** (4 projects - **High Priority**):
- `Chapter10\A_StoreViewerApplication` (4 mandatory issues)
- `Chapter10\B_DesigningForAutomaticBinding` (3 mandatory issues)
- `Chapter10\D_UsingDifferentEnvironments` (2 mandatory issues)
- `Chapter31\B_ConfigureOptionsExample` (2 mandatory issues)

**Deprecated Packages** (4 projects - **Medium Priority**):
- `Chapter32\D_FluentValidationConverter` - FluentValidation.AspNetCore deprecated
- `Chapter34\C_SystemdService` - Deprecated hosting extensions
- `Chapter34\D_WindowsService` - Deprecated hosting extensions
- `Chapter34\E_QuartzHostedService` - Deprecated hosting extensions
- `Chapter34\F_QuartzClustering` - Deprecated hosting extensions

**High Issue Count** (6+ issues - **Medium Priority**):
- Multiple RecipeApplication variants (50 issues each)
- Identity/Authentication projects (18-20 issues)
- ExchangeRateViewer, ConsoleClient projects (26-46 issues)

### Recommended Approach

**Incremental Migration** following Bottom-Up strategy:

**Phase 1**: High-risk projects (binary incompatible APIs) - 4 projects  
**Phase 2**: Deprecated package projects - 4 projects  
**Phase 3**: High-complexity projects (10+ issues) - batched by type  
**Phase 4**: Medium-complexity projects (3-9 issues) - batched  
**Phase 5**: Low-complexity projects (1-2 issues) - batched  
**Phase 6**: Test projects - 3 projects  

Each phase includes:
- Target framework update
- Package updates
- Breaking change resolution
- Build validation
- Test execution

### Expected Iterations

Following **Complex** classification strategy:
- **Phase 1 Iterations**: 4 iterations (1 per high-risk project)
- **Phase 2-3 Iterations**: 6-8 iterations (batching similar projects)
- **Phase 4-5 Iterations**: 2-4 iterations (larger batches)
- **Phase 6 Iteration**: 1 iteration (final test projects)
- **Final Iteration**: Success criteria and source control strategy

**Total Expected Iterations**: 13-17 iterations

---

## Migration Strategy

### Approach Selection: Incremental Migration

**Selected Approach**: Incremental, phased migration using **Bottom-Up (Dependency-First) Strategy**

**Rationale**:
- **Large solution**: 120 projects significantly exceeds threshold for all-at-once approach
- **Manageable complexity**: Despite large count, projects are independent with minimal coupling
- **Risk management**: Incremental approach allows isolation of issues per phase
- **Validation checkpoints**: Can pause and validate after each phase
- **Learning curve**: Lessons from early phases apply to later batches
- **Parallel capability**: Independent projects within phases can be processed simultaneously

### Bottom-Up Strategy Application

#### Strategy Principles

The Bottom-Up strategy prioritizes upgrading projects in dependency order, starting from leaf nodes (no dependencies) and progressing upward:

1. **Tier 0 First**: All 117 independent projects (no project dependencies)
2. **Tier 1 Second**: 3 test projects (depend only on Tier 0)
3. **Within Tiers**: Process by risk level and complexity, not alphabetically

**Benefits for This Solution**:
- **Minimal multi-targeting**: All Tier 0 projects are independent, no cross-project version conflicts
- **Clear validation points**: After Tier 0 complete, entire foundation is on .NET 10.0
- **Low risk**: Test projects upgrade last, when dependencies are stable
- **Efficient**: Flat structure allows aggressive batching within tiers

#### Strategy-Specific Considerations

**No Multi-Targeting Required**:
- All Tier 0 projects have no internal project dependencies
- Test projects can wait until Tier 0 complete
- No need for projects to target multiple frameworks simultaneously

**Batching Within Tiers**:
- Tier 0: Batch by risk and complexity, not project count
- Process 1-15 projects per batch depending on risk
- Tier 1: Process all 3 test projects together

**Risk-First Ordering Within Tier 0**:
Following Bottom-Up principles, within Tier 0:
1. **Binary incompatible APIs first** (highest risk, must establish baseline)
2. **Deprecated packages second** (high risk, may need research)
3. **High complexity third** (many issues, systematic approach needed)
4. **Medium/low complexity batched** (straightforward, aggressive batching)

### Dependency-Based Ordering Rationale

**Tier-Level Ordering**:
- **Tier 0 before Tier 1**: Test projects depend on application projects
- **No Tier 0 internal ordering required**: All projects are independent

**Risk-Based Ordering Within Tier 0**:
While all Tier 0 projects are technically equivalent (no dependencies), processing by risk level:
- **Establishes patterns**: Binary incompatible APIs reveal common issues
- **Reduces uncertainty**: High-risk projects completed early
- **Enables learning**: Lessons from complex projects apply to simpler ones
- **Optimizes batching**: Similar projects grouped efficiently

**Justification for Bottom-Up**:
- **Foundation first**: Independent projects form the foundation
- **Tests last**: Test projects depend on stable application projects
- **Stability**: Each tier validated before moving to next
- **No regression risk**: Lower tiers can't break when upper tiers change (no dependencies)

### Parallel vs Sequential Execution

#### Tier-Level Execution: **Sequential**

- **Tier 0 must complete before Tier 1 starts**
- Tier 1 test projects require their Tier 0 dependencies to be stable on .NET 10.0
- No parallelization across tiers

#### Within-Tier Execution: **Hybrid**

**Tier 0 (117 projects)**:
- **High-risk projects (Groups 0A, 0B)**: **Sequential** (8 projects)
  - Binary incompatible APIs: 1 at a time
  - Deprecated packages: 1 at a time or small batches
  - Reason: Require careful analysis and code changes

- **High-complexity projects (Group 0C)**: **Batched Sequential** (28 projects)
  - Process 3-5 similar projects per batch
  - Batches run sequentially, projects within batch can be parallel
  - Reason: Learn patterns from first batch, apply to subsequent batches

- **Medium-complexity projects (Group 0D)**: **Batched Parallel** (32 projects)
  - Process 5-8 projects per batch
  - Batches run sequentially, aggressive parallelization within batch
  - Reason: Straightforward changes, similar patterns

- **Low-complexity projects (Group 0E)**: **Batched Parallel** (53 projects)
  - Process 10-15 projects per batch
  - Maximum parallelization opportunity
  - Reason: Minimal issues, mostly framework updates

**Tier 1 (3 projects)**:
- **All together**: Process all 3 test projects in single batch
- Can run in parallel
- Reason: Simple test project updates, dependencies already stable

#### Resource Considerations

**Single-Threaded Constraints**:
- Build system limitations
- File system locking (project files, NuGet cache)
- Solution reload overhead

**Effective Parallelization**:
- Use logical batching (plan describes batch, execution processes sequentially)
- "Parallel" means "can be done in any order within batch"
- Actual execution may be sequential due to tooling constraints

### Phase Definitions

The migration is divided into **6 phases** following Bottom-Up strategy:

#### Phase 1: Critical - Binary Incompatible APIs (4 projects)

**Tier**: Tier 0, Group 0A  
**Execution**: Sequential (1 project per iteration)  
**Duration**: High complexity per project  

**Projects**:
1. `Chapter10\A_StoreViewerApplication\StoreViewerApplication`
2. `Chapter10\B_DesigningForAutomaticBinding\DesigningForAutomaticBinding`
3. `Chapter10\D_UsingDifferentEnvironments\UsingDifferentEnvironments`
4. `Chapter31\B_ConfigureOptionsExample\ConfigureOptionsExample`

**Rationale**: Binary incompatible APIs require code changes. Process first to establish patterns and avoid blocking later work.

#### Phase 2: High Priority - Deprecated Packages (4 projects)

**Tier**: Tier 0, Group 0B  
**Execution**: Sequential or small batches (2 projects per iteration)  
**Duration**: Medium complexity per project  

**Projects**:
1. `Chapter32\D_FluentValidationConverter\FluentValidationConverter`
2. `Chapter34\C_SystemdService\SystemdService`
3. `Chapter34\D_WindowsService\WindowsService`
4. `Chapter34\E_QuartzHostedService\QuartzHostedService`
5. `Chapter34\F_QuartzClustering\QuartzClustering`

**Rationale**: Deprecated packages may require alternative solutions or removal. Address early to avoid propagating deprecated patterns.

#### Phase 3: High Complexity - Identity & EF Core (28 projects)

**Tier**: Tier 0, Group 0C  
**Execution**: Batched sequential (3-5 projects per batch)  
**Duration**: Medium-high complexity per batch  

**Batches**:
- **Batch 3A**: Identity/Authentication projects (Chapter 23-24, 6 projects)
- **Batch 3B**: Recipe API projects with Auth (Chapter 25, 3 projects)
- **Batch 3C**: RecipeApplication variants (Chapter 12, 5 projects)
- **Batch 3D**: RecipeApplication with features (Chapter 22, 26, 30, 32, 36, 5 projects)
- **Batch 3E**: Identity RecipeApplication projects (Chapter 23-24, 6 projects)
- **Batch 3F**: ConsoleClient & ExchangeRateViewer (4 projects)

**Rationale**: Projects with many issues need systematic approach. Batching by technology type enables pattern reuse.

#### Phase 4: Medium Complexity - Standard Projects (32 projects)

**Tier**: Tier 0, Group 0D  
**Execution**: Batched parallel (5-8 projects per batch)  
**Duration**: Medium complexity per batch  

**Batches**:
- **Batch 4A**: Web API projects (Chapter 11, 20, 7 projects)
- **Batch 4B**: Tag Helpers & Razor Pages (Chapter 16-18, 8 projects)
- **Batch 4C**: Background services (Chapter 34, 2 projects)
- **Batch 4D**: Security & CORS examples (Chapter 28-29, 5 projects)
- **Batch 4E**: Miscellaneous ASP.NET Core (Chapter 19, 22, 31-33, 10 projects)

**Rationale**: Projects with moderate issues (3-9). Batch by project type for efficiency. Can process within batches in any order.

#### Phase 5: Low Complexity - Simple Examples (53 projects)

**Tier**: Tier 0, Group 0E  
**Execution**: Batched parallel (10-15 projects per batch)  
**Duration**: Low complexity per batch  

**Batches**:
- **Batch 5A**: Minimal API examples (Chapter 4-5, 13 projects)
- **Batch 5B**: Routing & Model Binding (Chapter 6-7, 14, 5 projects)
- **Batch 5C**: Dependency Injection examples (Chapter 8-9, 6 projects)
- **Batch 5D**: Configuration examples (Chapter 10, 13, 15, 12 projects)
- **Batch 5E**: Razor Pages basics (Chapter 13-15, 17, 17 projects)

**Rationale**: Projects with 1-2 issues (mostly framework change). Aggressive batching for efficiency. Mostly straightforward updates.

#### Phase 6: Test Projects (3 projects)

**Tier**: Tier 1  
**Execution**: Batch all together  
**Duration**: Low complexity  

**Projects**:
1. `Chapter35\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests`
2. `Chapter36\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests`
3. `Chapter36\B_RecipeApplication_SQLite\test\RecipeApplication.Tests`

**Rationale**: Test projects depend on Tier 0 applications. Process last when all dependencies are stable. Simple updates.

### Phase Transition Criteria

**Between Phases**:
1. All projects in phase build successfully
2. All projects in phase pass their tests (if applicable)
3. No compilation errors or warnings
4. Spot-check functionality for sample applications
5. Document lessons learned for next phase

**Between Tiers** (After Phase 5 → Before Phase 6):
1. All Tier 0 projects (117) build successfully
2. Dependent projects verified:
   - `ExchangeRates.Web` (Chapter 35 & 36) stable
   - `RecipeApplication` (Chapter 36) stable
3. No regressions detected in earlier phases
4. Ready to upgrade test projects

### Rollback Strategy

**Phase-Level Rollback**:
- Each phase is a logical unit
- If phase fails: Fix issues within phase or rollback entire phase
- Other phases remain stable

**Tier-Level Rollback**:
- If Tier 0 has systemic issues: Rollback entire tier (unlikely with phased approach)
- If Tier 1 fails: Rollback only Tier 1, Tier 0 remains on .NET 10.0

**Project-Level Rollback**:
- Within high-risk phases: Can rollback individual projects
- Within batches: Can rollback entire batch if issues discovered

**Implementation**:
- Git branch per phase (optional)
- Commit after each phase completes
- Tag major milestones (Tier 0 complete, Tier 1 complete)

### Success Criteria Per Phase

Each phase must meet these criteria before proceeding:

1. ✅ All projects in phase target .NET 10.0
2. ✅ All required packages updated to .NET 10.0 versions
3. ✅ All projects build without errors
4. ✅ All projects build without warnings (or documented exceptions)
5. ✅ All applicable tests pass
6. ✅ No new security vulnerabilities introduced
7. ✅ Breaking changes documented and resolved
8. ✅ Phase validated and committed to source control

---

## Detailed Dependency Analysis

### Dependency Graph Structure

The solution has a **flat, shallow dependency structure** ideal for parallel processing:

```
Tier 0 (117 projects - no internal project dependencies):
  ├─ Chapter 3-34: Sample application projects
  ├─ Chapter 35-36: Application projects with test dependencies
  └─ All standalone, self-contained examples

Tier 1 (3 projects - depend only on Tier 0):
  ├─ ExchangeRates.Web.Tests (Chapter 35) → depends on ExchangeRates.Web
  ├─ ExchangeRates.Web.Tests (Chapter 36) → depends on ExchangeRates.Web
  └─ RecipeApplication.Tests (Chapter 36) → depends on RecipeApplication
```

### Tier Breakdown

#### Tier 0: Independent Projects (117 projects)

All 117 projects have **no internal project dependencies**. They are self-contained chapter examples with only external NuGet dependencies. This enables:
- **Parallel processing**: Can upgrade multiple projects simultaneously
- **Independent validation**: Each project can be tested in isolation
- **Flexible batching**: Can group by complexity, risk, or project type

**Sub-grouping by Risk and Complexity**:

**Group 0A - Binary Incompatible APIs (4 projects - CRITICAL)**:
- `Chapter10\A_StoreViewerApplication\StoreViewerApplication` (4 mandatory issues)
- `Chapter10\B_DesigningForAutomaticBinding\DesigningForAutomaticBinding` (3 mandatory issues)
- `Chapter10\D_UsingDifferentEnvironments\UsingDifferentEnvironments` (2 mandatory issues)
- `Chapter31\B_ConfigureOptionsExample\ConfigureOptionsExample` (2 mandatory issues)

**Group 0B - Deprecated Packages (4 projects - HIGH)**:
- `Chapter32\D_FluentValidationConverter\FluentValidationConverter` (FluentValidation.AspNetCore)
- `Chapter34\C_SystemdService\SystemdService` (hosting extensions)
- `Chapter34\D_WindowsService\WindowsService` (hosting extensions)
- `Chapter34\E_QuartzHostedService\QuartzHostedService` (hosting extensions)
- `Chapter34\F_QuartzClustering\QuartzClustering` (hosting extensions)

**Group 0C - High Complexity (28 projects - 10+ issues)**:
- Identity/Authentication projects (12 projects): 18-50 issues each
- Entity Framework projects (12 projects): 50 issues each
- ConsoleClient projects (3 projects): 26-46 issues
- ExchangeRateViewer (1 project): 29 issues

**Group 0D - Medium Complexity (32 projects - 3-9 issues)**:
- Web API projects with package updates
- Blazor WebAssembly projects
- Background service projects
- Various ASP.NET Core examples

**Group 0E - Low Complexity (53 projects - 1-2 issues)**:
- Simple Razor Pages examples
- Minimal API examples
- Basic routing/middleware examples
- Configuration examples

#### Tier 1: Test Projects (3 projects)

**Dependencies**: Each test project depends on one Tier 0 application project

**Projects**:
1. `Chapter35\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests`
   - Depends on: `Chapter35\A_ExchangeRatesWeb\src\ExchangeRates.Web`
   - Issues: 1 (target framework only)

2. `Chapter36\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests`
   - Depends on: `Chapter36\A_ExchangeRatesWeb\src\ExchangeRates.Web`
   - Issues: 12 (includes package updates)

3. `Chapter36\B_RecipeApplication_SQLite\test\RecipeApplication.Tests`
   - Depends on: `Chapter36\B_RecipeApplication_SQLite\src\RecipeApplication`
   - Issues: 3 (includes package updates)

**Upgrade Order**: Must upgrade after their dependent Tier 0 projects complete

### Critical Path Identification

**Primary Critical Path**: None (flat structure)

**Sequential Dependencies**:
1. Tier 0 projects → Tier 1 test projects (3 dependencies)

**Recommended Processing Order**:
1. **Tier 0 Group 0A**: Binary incompatible APIs first (MUST FIX FIRST)
2. **Tier 0 Group 0B**: Deprecated packages (HIGH PRIORITY)
3. **Tier 0 Groups 0C-0E**: Batch by complexity (PARALLEL POSSIBLE)
4. **Tier 1**: Test projects after their dependencies (SEQUENTIAL)

### Circular Dependencies

**None detected**. The solution has a clean, acyclic dependency graph.

### Migration Phase Definitions

Based on Bottom-Up strategy and dependency analysis, projects are organized into these migration phases:

**Phase 1: Critical - Binary Incompatible APIs**
- 4 projects from Tier 0 Group 0A
- Must complete first due to breaking changes
- Individual processing recommended

**Phase 2: High Priority - Deprecated Packages**
- 4 projects from Tier 0 Group 0B
- Address deprecated dependencies early
- Can batch by package type

**Phase 3: High Complexity - Identity & EF Core**
- 28 projects from Tier 0 Group 0C
- Batch by technology (Identity, EF Core, etc.)
- 3-5 projects per batch

**Phase 4: Medium Complexity - Standard Projects**
- 32 projects from Tier 0 Group 0D
- Batch by project type (API, Blazor, Workers)
- 5-8 projects per batch

**Phase 5: Low Complexity - Simple Examples**
- 53 projects from Tier 0 Group 0E
- Large batches (10-15 projects)
- Straightforward framework updates

**Phase 6: Test Projects**
- 3 projects from Tier 1
- Upgrade after parent projects complete
- Process all together

### Dependency Validation Strategy

**Between-Tier Validation**:
- After Tier 0 complete: Verify all 117 projects build and test successfully
- Before Tier 1 start: Confirm dependent projects (ExchangeRates.Web, RecipeApplication) are stable

**Within-Tier Validation**:
- After each phase: Build all phase projects
- Smoke test critical functionality
- Verify no regressions in completed projects

### Risk Factors

**Low Overall Risk**:
- No circular dependencies
- Minimal inter-project coupling
- Clear upgrade path

**Specific Risks**:
- Binary incompatible APIs (4 projects) - requires careful code changes
- Deprecated packages (4 projects) - may need alternative solutions
- Test projects depend on already-upgraded apps - version alignment critical

---

## Project-by-Project Plans

### Phase 1: Critical - Binary Incompatible APIs

#### Project 1.1: StoreViewerApplication

**Location**: `Chapter10\A_StoreViewerApplication\StoreViewerApplication\StoreViewerApplication.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: ASP.NET Core Web Application
- Files: 4
- Issues: 4 (4 mandatory - all binary incompatible)
- Dependencies: None (standalone)
- **Breaking API**: `OptionsConfigurationServiceCollectionExtensions.Configure<T>(IServiceCollection, IConfiguration)` (3 occurrences)

**Target State**:
- Target Framework: net10.0
- Updated packages: None required

**Migration Steps**:

1. **Prerequisites**
   - Ensure .NET 10.0 SDK installed
   - Review Microsoft docs on Configuration.Bind API changes
   - Backup current code

2. **Update Target Framework**
   ```xml
   <!-- Before -->
   <TargetFramework>net7.0</TargetFramework>

   <!-- After -->
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Fix Binary Incompatible API (Program.cs, lines 4-6)**

   **Issue**: `Configure<T>(IServiceCollection, IConfiguration)` overload removed in .NET 10.0

   **Current Code**:
   ```csharp
   builder.Services.Configure<MapSettings>(builder.Configuration.GetSection("MapSettings"));
   builder.Services.Configure<AppDisplaySettings>(builder.Configuration.GetSection("AppDisplaySettings"));
   builder.Services.Configure<List<Store>>(builder.Configuration.GetSection("Stores"));
   ```

   **Replacement Options**:

   **Option A - Use BindConfiguration** (Recommended):
   ```csharp
   builder.Services.AddOptions<MapSettings>()
       .BindConfiguration("MapSettings");
   builder.Services.AddOptions<AppDisplaySettings>()
       .BindConfiguration("AppDisplaySettings");
   builder.Services.AddOptions<List<Store>>()
       .BindConfiguration("Stores");
   ```

   **Option B - Use Configure with Action delegate**:
   ```csharp
   builder.Services.Configure<MapSettings>(options => 
       builder.Configuration.GetSection("MapSettings").Bind(options));
   builder.Services.Configure<AppDisplaySettings>(options => 
       builder.Configuration.GetSection("AppDisplaySettings").Bind(options));
   builder.Services.Configure<List<Store>>(options => 
       builder.Configuration.GetSection("Stores").Bind(options));
   ```

   **Option C - Manual registration**:
   ```csharp
   var mapSettings = new MapSettings();
   builder.Configuration.GetSection("MapSettings").Bind(mapSettings);
   builder.Services.AddSingleton(Microsoft.Extensions.Options.Options.Create(mapSettings));
   // Repeat for other options
   ```

   **Recommended**: Option A (BindConfiguration) - cleanest .NET 10.0 pattern

4. **Expected Breaking Changes**
   - **API Change**: `Configure<T>(IServiceCollection, IConfiguration)` removed
   - **Behavior**: Functionality unchanged, only registration method differs
   - **Reason**: .NET 10.0 promotes more explicit configuration binding

5. **Code Modifications Required**
   - File: `Program.cs`
   - Lines: 4, 5, 6
   - Type: Replace method calls with new API pattern

6. **Testing Strategy**
   - **Build**: Verify project compiles without errors
   - **Configuration Loading**: Test that MapSettings, AppDisplaySettings, List<Store> correctly populated from appsettings.json
   - **Dependency Injection**: Verify IOptions<T> injection works in controllers/pages
   - **Functional**: Navigate application, verify stores display correctly
   - **Edge Cases**: Test with missing config sections, invalid JSON

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Project builds without warnings
   - [ ] Configuration values loaded correctly
   - [ ] IOptions<MapSettings> injectable and populated
   - [ ] IOptions<AppDisplaySettings> injectable and populated
   - [ ] IOptions<List<Store>> injectable and populated
   - [ ] Application runs and displays stores
   - [ ] No behavioral changes observed

---

#### Project 1.2: DesigningForAutomaticBinding

**Location**: `Chapter10\B_DesigningForAutomaticBinding\DesigningForAutomaticBinding\DesigningForAutomaticBinding.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: ASP.NET Core Web Application
- Issues: 3 (3 mandatory - all binary incompatible)
- Dependencies: None (standalone)
- **Breaking API**: Same as StoreViewerApplication - `Configure<T>` overload removed

**Target State**:
- Target Framework: net10.0
- Updated packages: None required

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Review StoreViewerApplication migration (same API issue)

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Fix Binary Incompatible API**

   **Issue**: `Configure<T>(IServiceCollection, IConfiguration)` overload removed

   **Solution**: Apply same pattern as StoreViewerApplication

   Replace all `builder.Services.Configure<T>(builder.Configuration.GetSection("..."))` calls with:
   ```csharp
   builder.Services.AddOptions<T>()
       .BindConfiguration("SectionName");
   ```

4. **Expected Breaking Changes**
   - Same as Project 1.1 (StoreViewerApplication)
   - Configuration binding API change

5. **Testing Strategy**
   - Build validation
   - Configuration loading tests
   - Model binding validation (project focuses on binding patterns)
   - Test automatic binding scenarios

6. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Configuration sections bind correctly
   - [ ] Model binding examples work as expected
   - [ ] No functional regressions

**Note**: This project demonstrates model binding patterns. Extra validation needed to ensure binding behavior unchanged.

---

#### Project 1.3: UsingDifferentEnvironments

**Location**: `Chapter10\D_UsingDifferentEnvironments\UsingDifferentEnvironments\UsingDifferentEnvironments.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: ASP.NET Core Web Application
- Issues: 2 (2 mandatory - all binary incompatible)
- Dependencies: None (standalone)
- **Breaking API**: Same `Configure<T>` API issue (2 occurrences)

**Target State**:
- Target Framework: net10.0
- Updated packages: None required

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Review StoreViewerApplication migration pattern

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Fix Binary Incompatible API (2 occurrences)**

   Apply same fix as Projects 1.1 and 1.2:
   ```csharp
   // Replace Configure<T>(config) with:
   builder.Services.AddOptions<T>()
       .BindConfiguration("SectionName");
   ```

4. **Environment-Specific Testing**

   **Critical**: This project demonstrates environment-based configuration

   Test all environments:
   - Development
   - Staging
   - Production

   Verify configuration loading:
   - appsettings.json
   - appsettings.Development.json
   - appsettings.Staging.json
   - appsettings.Production.json

5. **Testing Strategy**
   - Build validation
   - Test each environment configuration
   - Verify environment-specific settings override base settings
   - Test ASPNETCORE_ENVIRONMENT variable handling

6. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Development environment configuration loads correctly
   - [ ] Staging environment configuration loads correctly
   - [ ] Production environment configuration loads correctly
   - [ ] Environment-specific overrides work
   - [ ] Application behavior correct per environment

**Note**: Extra validation needed for environment-specific configuration loading.

---

#### Project 1.4: ConfigureOptionsExample

**Location**: `Chapter31\B_ConfigureOptionsExample\ConfigureOptionsExample\ConfigureOptionsExample.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: ASP.NET Core Web Application
- Issues: 2 (2 mandatory - all binary incompatible)
- Dependencies: None (standalone)
- **Breaking API**: Same `Configure<T>` API issue

**Target State**:
- Target Framework: net10.0
- Updated packages: None required

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Review established pattern from Projects 1.1-1.3

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Fix Binary Incompatible API**

   **Context**: This project specifically demonstrates options pattern configuration

   Apply same fix as previous projects:
   ```csharp
   builder.Services.AddOptions<T>()
       .BindConfiguration("SectionName");
   ```

   **Additional Consideration**: Project may demonstrate multiple options pattern techniques:
   - IOptions<T>
   - IOptionsSnapshot<T>
   - IOptionsMonitor<T>

   Ensure all patterns still work after migration.

4. **Options Pattern Validation**

   Test all options pattern variations:
   - **IOptions<T>**: Singleton, read once at startup
   - **IOptionsSnapshot<T>**: Scoped, reloaded per request
   - **IOptionsMonitor<T>**: Singleton, reloads on configuration change

   Verify:
   - Configuration binding works for all types
   - Lifetime semantics unchanged
   - Change notifications work (IOptionsMonitor)

5. **Testing Strategy**
   - Build validation
   - Test all IOptions* variations
   - Test configuration reloading scenarios
   - Verify post-configuration and validation hooks (if used)

6. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] IOptions<T> injection works
   - [ ] IOptionsSnapshot<T> injection works
   - [ ] IOptionsMonitor<T> injection works
   - [ ] Configuration binding correct
   - [ ] Configuration change notifications work (if applicable)
   - [ ] PostConfigure and Validate options work (if applicable)

**Note**: This project demonstrates options pattern extensively. Thorough validation required for all options variations.

---

### Phase 2: High Priority - Deprecated Packages

#### Project 2.1: FluentValidationConverter

**Location**: `Chapter32\D_FluentValidationConverter\FluentValidationConverter\FluentValidationConverter.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: ASP.NET Core Web Application (Razor Pages)
- Issues: 3 (1 mandatory, 2 potential)
- **Deprecated Package**: FluentValidation.AspNetCore 11.2.2
- **Behavioral Change**: UseExceptionHandler API
- Dependencies: None (standalone)

**Target State**:
- Target Framework: net10.0
- **Package Decision Required**: Remove deprecated FluentValidation.AspNetCore

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Review FluentValidation migration guide: https://docs.fluentvalidation.net/en/latest/aspnet.html
   - **Decision Point**: Choose migration strategy

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Deprecated Package Migration**

   **Issue**: FluentValidation.AspNetCore is deprecated

   **Reason**: ASP.NET Core integration features moved to base FluentValidation package

   **Migration Options**:

   **Option A - Migrate to FluentValidation base package** (Recommended):
   ```xml
   <!-- Remove -->
   <PackageReference Include="FluentValidation.AspNetCore" Version="11.2.2" />

   <!-- Add -->
   <PackageReference Include="FluentValidation" Version="11.9.0" />
   <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.9.0" />
   ```

   Update Program.cs:
   ```csharp
   // Remove (if present):
   // builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

   // Replace with:
   builder.Services.AddValidatorsFromAssemblyContaining<Program>();

   // Manual validation in code:
   // Inject IValidator<T> and call ValidateAsync(model)
   ```

   **Option B - Keep deprecated package temporarily**:
   ```xml
   <!-- Update to latest deprecated version -->
   <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
   ```
   Note: Not recommended for long-term

   **Option C - Remove FluentValidation entirely**:
   - Remove package
   - Replace with DataAnnotations validation (built-in)
   - Rewrite validators using [Required], [Range], etc.

4. **Code Changes for Option A** (Recommended)

   **If automatic validation was used**:
   - FluentValidation.AspNetCore provided automatic model validation
   - In .NET 10.0 with base package, must manually validate:

   ```csharp
   // In PageModel or Controller
   private readonly IValidator<MyModel> _validator;

   public MyPageModel(IValidator<MyModel> validator)
   {
       _validator = validator;
   }

   public async Task<IActionResult> OnPostAsync()
   {
       var result = await _validator.ValidateAsync(Model);
       if (!result.IsValid)
       {
           foreach (var error in result.Errors)
           {
               ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
           }
           return Page();
       }
       // Process valid model
   }
   ```

   **If validators registered individually**:
   - Replace individual registrations with `AddValidatorsFromAssembly`

5. **Fix Behavioral Change (Program.cs, line 20)**

   **Issue**: `UseExceptionHandler` has behavioral change in .NET 10.0

   **Current Code**:
   ```csharp
   app.UseExceptionHandler("/Error");
   ```

   **Action**: No code change needed, but verify behavior:
   - Exception handling may redirect differently
   - Test exception scenarios to ensure error page displays correctly
   - Review any custom exception handling logic

6. **Testing Strategy**
   - **Build**: Verify project compiles
   - **Validator Registration**: Ensure validators discovered and registered
   - **Validation Logic**: Test all validation rules still work
   - **Error Messages**: Verify error messages display correctly
   - **Model Binding**: Test validation fires on model binding
   - **Exception Handling**: Test error page displays on unhandled exceptions

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] FluentValidation package decision documented
   - [ ] Validators registered correctly
   - [ ] All validation rules work as expected
   - [ ] Error messages displayed properly
   - [ ] Model state errors populated correctly
   - [ ] Exception handler redirects to /Error correctly
   - [ ] No functional regressions

**Recommendation**: Use Option A (migrate to base FluentValidation package). This aligns with FluentValidation's current direction and provides long-term support.

---

#### Project 2.2: SystemdService

**Location**: `Chapter34\C_SystemdService\SystemdService\SystemdService.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: Worker Service (.NET Core App)
- Issues: 15 (1 mandatory, 14 potential)
- **Deprecated Package**: Microsoft.Extensions.Hosting.Systemd 7.0.0
- **Note**: Package marked deprecated but still published for .NET 10.0
- Dependencies: None (standalone)

**Target State**:
- Target Framework: net10.0
- Updated packages: Microsoft.Extensions.Hosting.Systemd 7.0.0 → 10.0.3

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Linux environment with systemd for testing (or WSL)
   - Review systemd service configuration

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Update Deprecated Package**

   **Context**: Package marked deprecated but Microsoft still publishes .NET 10.0 version

   ```xml
   <!-- Before -->
   <PackageReference Include="Microsoft.Extensions.Hosting.Systemd" Version="7.0.0" />

   <!-- After -->
   <PackageReference Include="Microsoft.Extensions.Hosting.Systemd" Version="10.0.3" />
   ```

   **Deprecation Note**: Package functionality works but Microsoft recommends alternative approaches in future .NET versions. For .NET 10.0, update is safe.

4. **Code Review (Program.cs)**

   Verify systemd integration code:
   ```csharp
   var builder = Host.CreateDefaultBuilder(args)
       .UseSystemd() // This call may have behavioral changes
       .ConfigureServices(services =>
       {
           services.AddHostedService<Worker>();
       });
   ```

   **Behavioral Changes**: Review for:
   - Service lifetime management changes
   - Logging integration differences
   - Signal handling (SIGTERM, SIGINT) behavior

5. **Systemd Service Configuration**

   Verify .service file still compatible:
   ```ini
   [Unit]
   Description=.NET Worker Service

   [Service]
   Type=notify
   ExecStart=/path/to/SystemdService

   [Install]
   WantedBy=multi-user.target
   ```

   Note: Type=notify is critical for systemd integration

6. **Testing Strategy**
   - **Build**: Verify project compiles
   - **Local Run**: Test service runs locally (dotnet run)
   - **Systemd Integration**: Deploy to Linux with systemd
   - **Service Lifecycle**: Test start, stop, restart, status
   - **Logging**: Verify logs written to systemd journal
   - **Signals**: Test SIGTERM and SIGINT handling
   - **Failure Handling**: Test service restart on failure

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Package updated to 10.0.3
   - [ ] Service runs locally
   - [ ] systemd recognizes service (systemctl status)
   - [ ] Service starts/stops correctly
   - [ ] Logs visible in journalctl
   - [ ] SIGTERM triggers graceful shutdown
   - [ ] Worker tasks execute correctly
   - [ ] No behavioral regressions

**Note**: While package is deprecated, it's fully functional for .NET 10.0. Plan for future migration to alternative systemd integration approach in later .NET versions.

---

#### Project 2.3: WindowsService

**Location**: `Chapter34\D_WindowsService\WindowsService\WindowsService.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: Worker Service (.NET Core App)
- Issues: 15 (1 mandatory, 14 potential)
- **Deprecated Package**: Microsoft.Extensions.Hosting.WindowsServices 7.0.0
- **Note**: Package marked deprecated but still published for .NET 10.0
- Dependencies: None (standalone)

**Target State**:
- Target Framework: net10.0
- Updated packages: Microsoft.Extensions.Hosting.WindowsServices 7.0.0 → 10.0.3

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Windows machine for testing
   - Administrator privileges (for service installation)
   - sc.exe or PowerShell for service management

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Update Deprecated Package**

   ```xml
   <!-- Before -->
   <PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" Version="7.0.0" />

   <!-- After -->
   <PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" Version="10.0.3" />
   ```

   **Deprecation Note**: Similar to SystemdService, package functional but marked deprecated for future migration.

4. **Code Review (Program.cs)**

   Verify Windows Service integration:
   ```csharp
   var builder = Host.CreateDefaultBuilder(args)
       .UseWindowsService() // May have behavioral changes
       .ConfigureServices(services =>
       {
           services.AddHostedService<Worker>();
       });
   ```

   **Behavioral Changes**: Review for:
   - Service Control Manager (SCM) integration changes
   - Event Log logging behavior
   - Service stop/pause handling
   - Console vs service mode detection

5. **Windows Service Installation**

   Publish and install:
   ```powershell
   # Publish
   dotnet publish -c Release -o C:\MyServices\WindowsService

   # Install
   sc.exe create "MyWorkerService" binPath="C:\MyServices\WindowsService\WindowsService.exe"

   # Or using PowerShell
   New-Service -Name "MyWorkerService" -BinaryPathName "C:\MyServices\WindowsService\WindowsService.exe"
   ```

6. **Testing Strategy**
   - **Build**: Verify project compiles
   - **Console Mode**: Test runs in console (dotnet run)
   - **Publish**: Verify publish succeeds
   - **Service Installation**: Install as Windows Service
   - **Service Lifecycle**: Start, stop, restart service
   - **Event Log**: Verify logs written to Windows Event Log
   - **Service Recovery**: Test automatic restart on failure
   - **Worker Execution**: Verify background tasks run correctly

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Package updated to 10.0.3
   - [ ] Runs in console mode (dotnet run)
   - [ ] Publishes successfully
   - [ ] Installs as Windows Service
   - [ ] Service starts/stops via SCM
   - [ ] Logs visible in Event Viewer
   - [ ] Worker tasks execute in service mode
   - [ ] Graceful shutdown on service stop
   - [ ] No behavioral regressions

**Testing Tips**:
- Use `sc.exe query "MyWorkerService"` to check service status
- Check Event Viewer → Application logs for service events
- Test both console and service modes

**Note**: Package deprecated but fully functional for .NET 10.0. Consider future migration to alternative Windows Service hosting approach.

---

#### Project 2.4: QuartzHostedService

**Location**: `Chapter34\E_QuartzHostedService\QuartzHostedService\QuartzHostedService.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: Worker Service (.NET Core App)
- Issues: 16 (1 mandatory, 15 potential)
- **Packages**: Quartz.Extensions.Hosting 3.5.0, Microsoft.Extensions.Hosting 7.0.0
- **Source Incompatible APIs**: Present (need investigation)
- Dependencies: None (standalone)

**Target State**:
- Target Framework: net10.0
- Updated packages: Microsoft.Extensions.Hosting 7.0.0 → 10.0.3
- Quartz packages: Verify compatibility, update if needed

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Review Quartz.NET 3.x documentation
   - Check Quartz.NET .NET 10.0 compatibility

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Update Packages**

   ```xml
   <!-- Update hosting package -->
   <PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.3" />

   <!-- Check Quartz.NET compatibility -->
   <PackageReference Include="Quartz.Extensions.Hosting" Version="3.5.0" />
   <PackageReference Include="Quartz.Serialization.Json" Version="3.5.0" />
   ```

   **Note**: Quartz 3.5.0 should be compatible with .NET 10.0, but verify in testing.

4. **Address Source Incompatible APIs**

   **Potential Issues**:
   - Quartz scheduler API changes
   - Job scheduling method signature changes
   - Trigger configuration changes
   - Hosted service lifecycle changes

   **Action**: Compile project, address any API changes discovered

5. **Code Review (Program.cs)**

   Typical Quartz hosted service setup:
   ```csharp
   var builder = Host.CreateDefaultBuilder(args)
       .ConfigureServices((hostContext, services) =>
       {
           services.AddQuartz(q =>
           {
               q.UseMicrosoftDependencyInjectionJobFactory();
               // Job configuration
           });
           services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
       });
   ```

   Verify:
   - AddQuartz configuration still valid
   - Job factory registration unchanged
   - Hosted service options valid

6. **Testing Strategy**
   - **Build**: Verify compilation
   - **Job Scheduling**: Test jobs schedule correctly
   - **Job Execution**: Verify jobs execute on schedule
   - **Dependency Injection**: Test DI in job classes
   - **Graceful Shutdown**: Verify WaitForJobsToComplete works
   - **Error Handling**: Test job failure scenarios
   - **Logging**: Verify Quartz logs integrated

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Packages updated
   - [ ] Quartz scheduler initializes
   - [ ] Jobs schedule successfully
   - [ ] Jobs execute on time
   - [ ] DI works in job classes
   - [ ] Graceful shutdown waits for jobs
   - [ ] Job persistence works (if configured)
   - [ ] No behavioral regressions

---

#### Project 2.5: QuartzClustering

**Location**: `Chapter34\F_QuartzClustering\QuartzClustering\QuartzClustering.csproj`

**Current State**:
- Target Framework: net7.0
- Project Type: Worker Service (.NET Core App)
- Issues: 15 (1 mandatory, 14 potential)
- **Packages**: Quartz.Extensions.Hosting, Quartz.Serialization.Json, Microsoft.Extensions.Hosting
- **Feature**: Demonstrates Quartz clustering with persistent job store
- Dependencies: None (standalone)

**Target State**:
- Target Framework: net10.0
- Updated packages: All Quartz and hosting packages to .NET 10.0 versions

**Migration Steps**:

1. **Prerequisites**
   - .NET 10.0 SDK installed
   - Database for job persistence (SQL Server, PostgreSQL, etc.)
   - Multiple instances for cluster testing
   - Review QuartzHostedService migration (similar issues)

2. **Update Target Framework**
   ```xml
   <TargetFramework>net10.0</TargetFramework>
   ```

3. **Update Packages**

   ```xml
   <PackageReference Include="Microsoft.Extensions.Hosting" Version="10.0.3" />
   <PackageReference Include="Quartz.Extensions.Hosting" Version="3.5.0" />
   <PackageReference Include="Quartz.Serialization.Json" Version="3.5.0" />
   <!-- Database provider packages as needed -->
   ```

4. **Clustering Configuration Review**

   Typical clustering setup:
   ```csharp
   services.AddQuartz(q =>
   {
       q.UsePersistentStore(store =>
       {
           store.UseProperties = true;
           store.UseClustering(c =>
           {
               c.CheckinInterval = TimeSpan.FromSeconds(20);
               c.CheckinMisfireThreshold = TimeSpan.FromSeconds(30);
           });
           store.UseSqlServer(connectionString);
           store.UseJsonSerializer();
       });
   });
   ```

   Verify:
   - Persistent store configuration valid
   - Clustering settings unchanged
   - Database provider compatibility with .NET 10.0
   - JSON serialization works

5. **Database Schema**

   **Action**: Verify Quartz database schema compatible
   - Check if schema updates needed for Quartz 3.5.0
   - Test database connection with .NET 10.0
   - Verify clustering tables accessible

6. **Testing Strategy**
   - **Build**: Verify compilation
   - **Database Connection**: Test connection to persistent store
   - **Single Instance**: Verify single instance works
   - **Cluster Setup**: Start multiple instances
   - **Job Distribution**: Verify jobs distributed across cluster
   - **Failover**: Stop one instance, verify jobs move to others
   - **Checkin**: Verify instances check in to cluster
   - **Misfire**: Test misfire handling
   - **Serialization**: Verify job data serializes correctly

7. **Validation Checklist**
   - [ ] Project builds without errors
   - [ ] Packages updated
   - [ ] Database connection works
   - [ ] Quartz schema valid
   - [ ] Single instance schedules jobs
   - [ ] Multiple instances form cluster
   - [ ] Jobs execute across cluster nodes
   - [ ] Failover works (job moves to active node)
   - [ ] Checkin intervals respected
   - [ ] JSON serialization works
   - [ ] No data corruption in persistent store
   - [ ] No behavioral regressions

**Cluster Testing Tips**:
- Run 2-3 instances simultaneously
- Use different ports if running locally
- Monitor database for checkin timestamps
- Test job execution during node failures
- Verify only one node executes each job instance

**Note**: Clustering is the most complex Quartz scenario. Thorough testing required to ensure cluster coordination works correctly in .NET 10.0.

---

### Phase 3-6: Remaining Projects

Due to the large number of projects (111 remaining), detailed plans for Phases 3-6 will be filled in subsequent iterations. Projects are organized by:

**Phase 3**: High Complexity (28 projects) - Identity, EF Core, API projects with 10+ issues  
**Phase 4**: Medium Complexity (32 projects) - Standard ASP.NET Core projects with 3-9 issues  
**Phase 5**: Low Complexity (53 projects) - Simple examples with 1-2 issues  
**Phase 6**: Test Projects (3 projects) - Test projects depending on Phase 3-5 applications  

[Details to be filled in subsequent iterations]

---

## Risk Management

### High-Risk Changes

| Project | Risk Level | Description | Mitigation |
|---------|-----------|-------------|------------|
| **Chapter10\A_StoreViewerApplication** | **Critical** | 4 binary incompatible API issues - requires code changes for compilation | Process first, document API migration patterns, thorough testing |
| **Chapter10\B_DesigningForAutomaticBinding** | **Critical** | 3 binary incompatible API issues - model binding changes | Follow StoreViewerApplication patterns, validate binding behavior |
| **Chapter10\D_UsingDifferentEnvironments** | **Critical** | 2 binary incompatible API issues - configuration changes | Test all environment configurations, verify appsettings loading |
| **Chapter31\B_ConfigureOptionsExample** | **Critical** | 2 binary incompatible API issues - options pattern changes | Validate options configuration, test dependency injection |
| **Chapter32\D_FluentValidationConverter** | **High** | Deprecated FluentValidation.AspNetCore package | Research migration path, consider removing package or migrating to FluentValidation base library |
| **Chapter34\C_SystemdService** | **High** | Deprecated hosting extensions, behavioral changes | Verify systemd integration still works, test service lifecycle |
| **Chapter34\D_WindowsService** | **High** | Deprecated hosting extensions, behavioral changes | Test Windows Service installation and lifecycle |
| **Chapter34\E_QuartzHostedService** | **High** | Deprecated hosting extensions, source incompatible APIs | Validate Quartz.NET scheduling behavior, test background tasks |
| **Chapter34\F_QuartzClustering** | **High** | Deprecated hosting extensions, clustering behavior changes | Test cluster coordination, verify persistence store |
| **Identity/Auth Projects (12 projects)** | **Medium-High** | 18-20 issues each, source incompatible APIs, behavioral changes | Batch processing, test authentication flows, verify token generation |
| **EF Core Projects (20+ projects)** | **Medium** | 50 issues each, many source incompatible APIs | Batch by EF Core scenario, test migrations, verify database operations |
| **ConsoleClient Projects (3)** | **Medium** | 26-46 issues each, HTTP client changes | Test API client generation, verify OpenAPI integration |

### Security Vulnerabilities

**Assessment Finding**: No security vulnerabilities identified in current NuGet packages.

**Post-Upgrade Validation**:
- Run `dotnet list package --vulnerable` after each phase
- Review deprecated packages for security implications
- Verify authentication/authorization behavior unchanged

### Contingency Plans

#### Binary Incompatible APIs (Phase 1 Projects)

**Risk**: APIs removed or signatures changed, code won't compile

**Mitigation**:
1. Query detailed API breakingchanges using assessment tools
2. Review Microsoft documentation for migration guidance
3. Search for replacement APIs in .NET 10.0
4. Implement code changes incrementally
5. Test thoroughly after each change

**Fallback**:
- If API has no replacement: Research workarounds or alternative patterns
- If blocking issue: Document and escalate, consider deferring project
- If partial success: Continue with successful projects, revisit failed ones

#### Deprecated Packages (Phase 2 Projects)

**Risk**: Packages may not have clear migration paths

**Mitigation**:
1. Research package documentation for deprecation guidance
2. Check if package functionality integrated into .NET 10.0
3. Explore alternative packages maintained for .NET 10.0
4. Test functionality after package changes

**Fallback Options**:

**FluentValidation.AspNetCore**:
- **Option A**: Migrate to base FluentValidation package (recommended)
- **Option B**: Remove FluentValidation entirely, use built-in validation
- **Option C**: Keep deprecated package temporarily if no critical issues (not recommended)

**Hosting Extensions (Systemd, WindowsServices)**:
- **Option A**: Update to .NET 10.0 versions (packages still published but marked deprecated)
- **Option B**: Use new .NET 10.0 hosting APIs if available
- **Option C**: Keep existing packages if no functional issues

**Quartz.NET Extensions**:
- **Option A**: Update Quartz.Extensions.Hosting to compatible version
- **Option B**: Configure Quartz directly without extension package
- **Option C**: Research alternative scheduling libraries

#### Performance Degradation

**Risk**: Behavioral changes cause performance regression

**Mitigation**:
- Baseline performance metrics before upgrade (optional for sample projects)
- Spot-check critical paths after upgrade
- Review .NET 10.0 performance improvements documentation
- Profile suspect areas if issues arise

**Fallback**:
- Investigate specific behavioral change causing regression
- Apply performance optimizations if needed
- Report issue to Microsoft if unexpected

#### Entity Framework Core Migration Issues

**Risk**: EF Core migrations fail or data access breaks

**Mitigation**:
- Test migrations in development environment
- Backup databases before running migrations (if using persistent data)
- Verify EF Core 10.0 compatibility with database providers (SQLite, SQL Server)
- Test CRUD operations after migration

**Fallback**:
- Regenerate migrations if corruption detected
- Manually fix migration code if auto-generation fails
- Use EF Core documentation for migration troubleshooting

#### Test Failures After Upgrade

**Risk**: Tests fail due to behavioral changes or API changes

**Mitigation**:
- Review test failures for root cause
- Distinguish between:
  - Test code needs update (test assertion incorrect)
  - Application code needs fix (behavioral change broke functionality)
  - Expected behavioral change (update test expectations)

**Fallback**:
- Fix test code to match .NET 10.0 behavior
- Update application code if behavioral change undesirable
- Document behavioral changes that affect tests

#### Blocking Issues

**Risk**: Critical issue prevents completing a phase

**Response Protocol**:
1. **Isolate**: Identify specific project(s) or issue(s) blocking progress
2. **Document**: Record issue details, error messages, attempted solutions
3. **Research**: Search Microsoft docs, GitHub issues, Stack Overflow
4. **Escalate**: Post question on forums, file GitHub issue if bug suspected
5. **Defer**: Mark project as "deferred," continue with non-blocked projects
6. **Revisit**: Return to blocked project after completing other work

**Decision Criteria for Deferral**:
- Issue affects < 5% of projects → Defer and continue
- Issue affects > 20% of projects → Stop and resolve
- Issue is sample project specific → Defer indefinitely
- Issue is systemic (affects core functionality) → Must resolve

### Risk Mitigation Timeline

| Phase | Primary Risks | Mitigation Actions | Timeline |
|-------|--------------|-------------------|----------|
| **Phase 1** | Binary incompatible APIs | Detailed API research, incremental changes, extensive testing | High (1 project at a time) |
| **Phase 2** | Deprecated packages | Package research, migration planning, alternative evaluation | Medium (2 projects per batch) |
| **Phase 3** | High complexity, many issues | Systematic batching, pattern identification, thorough testing | Medium (3-5 projects per batch) |
| **Phase 4** | Moderate issues, diverse project types | Efficient batching, shared patterns | Low (5-8 projects per batch) |
| **Phase 5** | Minimal issues, large project count | Aggressive batching, spot-check validation | Low (10-15 projects per batch) |
| **Phase 6** | Test project compatibility | Validate test frameworks, verify dependencies | Low (all together) |

### Success Indicators

**Green Flags** (proceed confidently):
- ✅ Projects build without errors
- ✅ Tests pass with expected results
- ✅ No new warnings introduced
- ✅ Behavioral changes documented and understood
- ✅ Performance remains acceptable
- ✅ Pattern established for batch replication

**Yellow Flags** (proceed with caution):
- ⚠️ Some warnings introduced (evaluate severity)
- ⚠️ Minor test failures (may need test updates)
- ⚠️ Undocumented behavioral changes (research needed)
- ⚠️ Performance slightly degraded (monitor)

**Red Flags** (stop and investigate):
- 🛑 Projects fail to build
- 🛑 Critical functionality broken
- 🛑 Major test suite failures
- 🛑 Security vulnerabilities introduced
- 🛑 Data loss or corruption
- 🛑 Severe performance degradation

### Rollback Procedures

**Phase-Level Rollback**:
```bash
# Rollback entire phase if critical issues found
git reset --hard <pre-phase-commit>
git clean -fd
```

**Selective Rollback** (within phase):
```bash
# Rollback specific project files
git checkout <pre-phase-commit> -- <project-path>/*.csproj
git checkout <pre-phase-commit> -- <project-path>/**/*.cs
```

**Tier-Level Rollback** (extreme scenario):
```bash
# Rollback entire tier (all phases)
git reset --hard <tier-start-commit>
git clean -fd
```

**Recovery Steps After Rollback**:
1. Document reason for rollback
2. Research issue resolution
3. Create fix/workaround
4. Re-attempt upgrade with corrections
5. Update plan if needed

---

## Testing & Validation Strategy

### Multi-Level Testing Approach

Following Bottom-Up strategy, testing occurs at three levels: per-project, per-phase, and per-tier.

---

### Level 1: Per-Project Testing

**Executed**: After each project upgrade completes  
**Scope**: Individual project in isolation  
**Goal**: Verify project successfully upgraded to .NET 10.0  

#### Standard Test Protocol

For every project, execute these validations:

**1. Build Validation**
```bash
dotnet build --configuration Release
```
- ✅ Build succeeds without errors
- ✅ Zero compilation errors
- ⚠️ Warnings reviewed and documented (acceptable) or fixed
- ✅ No missing references
- ✅ NuGet restore succeeds

**2. Package Verification**
```bash
dotnet list package
dotnet list package --vulnerable
```
- ✅ All packages restored
- ✅ Package versions correct (Framework packages at 10.0.x)
- ✅ No security vulnerabilities
- ✅ No deprecated package warnings (except documented exceptions)

**3. Project File Validation**
- ✅ TargetFramework = net10.0
- ✅ SDK version appropriate
- ✅ No obsolete project properties
- ✅ PackageReferences have correct versions

**4. Smoke Test (If Applicable)**

For runnable projects (Web, API, Worker):
```bash
dotnet run --configuration Release
```
- ✅ Application starts without errors
- ✅ No runtime exceptions on startup
- ✅ Basic functionality works (homepage loads, API responds, worker runs)
- ✅ Application stops gracefully (Ctrl+C)

**5. Unit Test Execution (If Applicable)**

For test projects:
```bash
dotnet test --configuration Release
```
- ✅ All tests run
- ✅ All tests pass (or documented exceptions)
- ✅ No test infrastructure errors
- ✅ Code coverage maintained (if tracked)

---

### Level 2: Per-Phase Testing

**Executed**: After all projects in a phase complete  
**Scope**: All projects within the phase  
**Goal**: Verify phase cohesion and no inter-project regressions  

#### Phase Testing Protocol

**Phase 1: Binary Incompatible APIs (4 projects)**

**Comprehensive Testing**:
- Build all 4 projects in single command: `dotnet build asp-dot-net-core-in-action-3e.sln`
- Run each project individually, test configuration binding
- Verify all `Configure<T>` replacements work
- Test options injection in each project
- Document API migration pattern for reference

**Phase Completion Criteria**:
- [ ] All 4 projects build without errors
- [ ] All projects run successfully
- [ ] Configuration binding works in all projects
- [ ] API migration pattern documented
- [ ] No warnings introduced

---

**Phase 2: Deprecated Packages (4 projects)**

**Comprehensive Testing**:
- Build all 4 projects together
- Test FluentValidation migration thoroughly
- Test systemd service on Linux (SystemdService)
- Test Windows Service installation (WindowsService)
- Test Quartz scheduling (both projects)
- Verify cluster coordination (QuartzClustering)

**Phase Completion Criteria**:
- [ ] All 4 projects build without errors
- [ ] FluentValidation migration decision implemented and tested
- [ ] SystemdService runs as systemd service
- [ ] WindowsService installs and runs as Windows Service
- [ ] Quartz jobs execute on schedule
- [ ] Quartz clustering works correctly
- [ ] Deprecated package strategy documented

---

**Phase 3: High Complexity (28 projects)**

**Batched Testing** (per batch):
- Build batch projects together
- Run sample projects from batch
- Test Entity Framework migrations (if applicable)
- Test Identity/authentication flows (if applicable)
- Verify database operations (if applicable)
- Validate API clients (ConsoleClient projects)

**Batch Testing Pattern**:
For each of 6 batches (3A-3F):
1. Build batch: `dotnet build` (filtered to batch projects)
2. Run 1-2 representative projects from batch
3. Test technology-specific features (EF Core, Identity, etc.)
4. Document patterns discovered
5. Apply patterns to remaining batch projects

**Phase Completion Criteria**:
- [ ] All 28 projects build without errors
- [ ] Representative projects from each batch tested
- [ ] EF Core migrations work (if applicable)
- [ ] Identity authentication flows work (if applicable)
- [ ] API projects respond correctly
- [ ] ConsoleClient projects connect to APIs
- [ ] No database errors
- [ ] Patterns documented per technology

---

**Phase 4: Medium Complexity (32 projects)**

**Batched Testing** (per batch):
- Build batch projects together
- Spot-check 1-2 projects per batch
- Verify behavioral changes acceptable
- Test project-type-specific features

**Batch Testing Pattern**:
For each of 5 batches (4A-4E):
1. Build batch
2. Run 1 project from batch (representative)
3. Basic functionality check
4. Verify no regressions
5. Move to next batch

**Phase Completion Criteria**:
- [ ] All 32 projects build without errors
- [ ] Spot-checks pass for each batch
- [ ] Web API projects respond correctly
- [ ] Razor Pages render correctly
- [ ] Blazor components work
- [ ] Background services execute tasks
- [ ] No functional regressions

---

**Phase 5: Low Complexity (53 projects)**

**Batched Testing** (per batch):
- Build batch projects together (10-15 projects)
- Spot-check 1-2 projects per batch
- Minimal testing (mostly build validation)

**Batch Testing Pattern**:
For each of 5 batches (5A-5E):
1. Build batch (verify compilation)
2. Run 1 project (basic smoke test)
3. Quick functional check
4. Move to next batch

**Phase Completion Criteria**:
- [ ] All 53 projects build without errors
- [ ] Spot-checks pass
- [ ] Minimal API examples work
- [ ] Basic Razor Pages render
- [ ] Simple applications run
- [ ] No build errors

---

**Phase 6: Test Projects (3 projects)**

**Testing**:
- Build all 3 test projects
- Run all test suites
- Verify tests pass with .NET 10.0
- Check test coverage maintained

**Phase Completion Criteria**:
- [ ] All 3 test projects build
- [ ] All test suites run
- [ ] All tests pass (or documented failures)
- [ ] Test frameworks compatible with .NET 10.0
- [ ] Dependencies (application projects) stable

---

### Level 3: Per-Tier Testing

**Executed**: After each tier completes (Tier 0, then Tier 1)  
**Scope**: All projects in the tier  
**Goal**: Verify entire tier stability before moving to next tier  

#### Tier 0 Validation (After Phase 5 Completes)

**Full Solution Build**:
```bash
dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release
```
- ✅ All 117 Tier 0 projects build successfully
- ✅ No compilation errors across solution
- ✅ No missing package references
- ✅ No project reference errors

**Comprehensive Validation**:
1. **Build Verification**: Full solution build succeeds
2. **Spot-Check Testing**: Run 10-15 representative projects across all phases
3. **Pattern Validation**: Verify established patterns applied consistently
4. **Regression Check**: Re-run Phase 1-2 critical projects
5. **Documentation Review**: Ensure all decisions and patterns documented

**Tier 0 Completion Criteria**:
- [ ] Full solution builds without errors
- [ ] All 117 Tier 0 projects on net10.0
- [ ] All packages updated appropriately
- [ ] Spot-check tests pass
- [ ] No regressions detected
- [ ] Critical functionality validated
- [ ] Tier 0 validated and committed to source control

**Checkpoint**: PAUSE - Do not proceed to Tier 1 until Tier 0 fully validated

---

#### Tier 1 Validation (After Phase 6 Completes)

**Test Projects Build**:
```bash
dotnet build --configuration Release
# For each test project
```
- ✅ All 3 test projects build successfully
- ✅ Dependencies (ExchangeRates.Web, RecipeApplication) on net10.0

**Test Execution**:
```bash
dotnet test --configuration Release
# For each test project
```
- ✅ Test runners compatible with .NET 10.0
- ✅ All test suites execute
- ✅ Test results documented

**Tier 1 Completion Criteria**:
- [ ] All 3 test projects build
- [ ] All test suites run
- [ ] Test results acceptable (pass or documented failures)
- [ ] Dependencies stable and compatible
- [ ] Test projects committed to source control

---

### Full Solution Testing (After Tier 1 Complete)

**Final Validation**:

**1. Complete Solution Build**
```bash
dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release
```
- ✅ All 120 projects build successfully
- ✅ Zero errors across entire solution
- ✅ Warnings documented and acceptable

**2. Package Audit**
```bash
dotnet list package --include-transitive
dotnet list package --vulnerable
dotnet list package --deprecated
```
- ✅ No security vulnerabilities
- ✅ Deprecated packages documented with justification
- ✅ All .NET packages at 10.0.x versions

**3. Comprehensive Smoke Testing**

Test representative projects from each category:
- **Web Applications**: 3-5 projects (different chapters)
- **Web APIs**: 2-3 projects
- **Blazor**: 1-2 projects
- **Worker Services**: 2-3 projects (including Quartz projects)
- **Console Apps**: 1-2 projects
- **Test Projects**: All 3

**4. Regression Testing**

Re-validate all Phase 1-2 critical projects:
- Binary incompatible API projects (4 projects)
- Deprecated package projects (4 projects)

**5. Documentation Validation**

Review and finalize:
- All breaking changes documented
- All API migrations recorded
- All package decisions justified
- All patterns documented
- Lessons learned captured

---

### Testing Tools and Commands

**Build Commands**:
```bash
# Single project
dotnet build <project>.csproj --configuration Release

# Multiple projects (filtered)
dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release -p:Projects="Chapter10\**"

# Entire solution
dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release
```

**Test Commands**:
```bash
# Single test project
dotnet test <test-project>.csproj --configuration Release

# All tests
dotnet test asp-dot-net-core-in-action-3e.sln --configuration Release

# With detailed output
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

**Run Commands**:
```bash
# Run web application
dotnet run --project <project>.csproj --configuration Release

# Run worker service
dotnet run --project <project>.csproj --configuration Release

# Run console app
dotnet run --project <project>.csproj --configuration Release
```

**Package Commands**:
```bash
# List packages
dotnet list package

# Check for vulnerabilities
dotnet list package --vulnerable

# Check for deprecated packages
dotnet list package --deprecated

# Include transitive dependencies
dotnet list package --include-transitive
```

---

### Test Failure Response Protocol

**When Tests Fail**:

1. **Isolate**: Identify which project(s) failing
2. **Categorize**: Determine failure type
   - Build error → Code fix needed
   - Test failure → Test or code fix needed
   - Runtime error → Behavioral change investigation needed
3. **Investigate**: Review error messages, stack traces, logs
4. **Research**: Check Microsoft docs, breaking changes, GitHub issues
5. **Fix**: Apply appropriate fix
6. **Retest**: Validate fix resolves issue
7. **Document**: Record issue and resolution

**Failure Categories**:

**Build Failures**:
- API not found → Replace with .NET 10.0 equivalent
- Signature mismatch → Update method call
- Missing package → Add or update package

**Test Failures**:
- Assertion failed → Review for behavioral change, update test or code
- Exception thrown → Investigate behavioral change
- Test infrastructure error → Update test framework packages

**Runtime Failures**:
- NullReferenceException → Review code for .NET 10.0 null handling changes
- InvalidOperationException → Review for behavioral changes
- Configuration error → Check configuration binding migration

---

### Success Metrics

**Build Metrics**:
- 100% of projects build successfully
- Zero compilation errors
- Warnings < 5% increase (documented)

**Test Metrics**:
- 100% of test projects build
- ≥ 95% of tests pass (failures documented and justified)
- No new test infrastructure errors

**Functional Metrics**:
- All smoke tests pass
- Representative projects run successfully
- Critical functionality validated

**Quality Metrics**:
- No security vulnerabilities introduced
- No deprecated packages (except documented)
- All breaking changes addressed
- Code quality maintained

---

## Complexity & Effort Assessment

### Per-Project Complexity

| Project Group | Project Count | Complexity Rating | Dependencies | Risk | Issues Range |
|--------------|---------------|------------------|--------------|------|--------------|
| **Phase 1: Binary Incompatible** | 4 | **High** | 0 | Critical | 2-4 mandatory |
| **Phase 2: Deprecated Packages** | 4 | **High** | 0 | High | 3-16 total |
| **Phase 3: High Complexity** | 28 | **Medium-High** | 0 | Medium | 10-50 issues |
| **Phase 4: Medium Complexity** | 32 | **Medium** | 0 | Low-Medium | 3-9 issues |
| **Phase 5: Low Complexity** | 53 | **Low** | 0 | Low | 1-2 issues |
| **Phase 6: Test Projects** | 3 | **Low** | 1 each | Low | 1-12 issues |

### Phase Complexity Assessment

#### Phase 1: Critical - Binary Incompatible APIs

**Complexity**: **High**  
**Rationale**:
- Binary incompatible APIs require code changes for compilation
- API replacements may not be straightforward
- Requires understanding of API functionality and context
- Testing required to ensure behavior unchanged

**Effort Factors**:
- Research replacement APIs: Medium
- Implement code changes: Medium-High
- Compilation fixes: High (blocking)
- Testing: Medium
- Documentation: Medium

**Dependency Ordering Impact**: **None** (all independent projects)

**Bottom-Up Strategy Application**:
- Process individually to establish patterns
- First project highest effort (learning), subsequent projects benefit from patterns
- Each project validates independently before proceeding

#### Phase 2: High Priority - Deprecated Packages

**Complexity**: **High**  
**Rationale**:
- Deprecated packages may require alternative solutions
- FluentValidation migration may need architectural changes
- Hosting extensions deprecation may affect service behavior
- Quartz.NET integration requires validation

**Effort Factors**:
- Research migration paths: High
- Package removal/replacement: Medium-High
- Code refactoring: Medium
- Testing service behavior: High
- Documentation: Medium

**Dependency Ordering Impact**: **None** (all independent projects)

**Bottom-Up Strategy Application**:
- Process individually or small batches
- FluentValidation separate from hosting extensions (different migration paths)
- Validate service lifecycle after hosting changes

#### Phase 3: High Complexity - Identity & EF Core

**Complexity**: **Medium-High**  
**Rationale**:
- High issue counts (10-50 per project)
- Source incompatible APIs require systematic resolution
- Entity Framework migrations need validation
- Identity/authentication flows require thorough testing
- Behavioral changes may affect functionality

**Effort Factors**:
- Issue volume: High
- Source incompatible API resolution: Medium
- EF Core migration testing: Medium
- Identity/authentication testing: High
- Behavioral change validation: Medium

**Dependency Ordering Impact**: **None** (all independent projects)

**Bottom-Up Strategy Application**:
- Batch by technology (Identity, EF Core, APIs)
- 3-5 projects per batch
- Establish patterns in first batch, replicate in subsequent batches
- Systematic approach to repetitive issues

**Batching Efficiency**:
- First batch: High effort (pattern discovery)
- Subsequent batches: Medium effort (pattern application)
- Similar projects within batch benefit from shared solutions

#### Phase 4: Medium Complexity - Standard Projects

**Complexity**: **Medium**  
**Rationale**:
- Moderate issue counts (3-9 per project)
- Mix of Web API, Razor Pages, Blazor projects
- Behavioral changes require validation
- Package updates straightforward

**Effort Factors**:
- Issue volume: Medium
- Package updates: Low-Medium
- Behavioral change validation: Medium
- Testing: Medium
- Code changes: Low-Medium

**Dependency Ordering Impact**: **None** (all independent projects)

**Bottom-Up Strategy Application**:
- Batch by project type (5-8 projects per batch)
- Parallel processing within batches possible
- Shared patterns across similar project types

**Batching Efficiency**:
- Web API projects share patterns
- Razor Pages projects share patterns
- Background services share patterns
- Efficient processing with established patterns from Phase 3

#### Phase 5: Low Complexity - Simple Examples

**Complexity**: **Low**  
**Rationale**:
- Minimal issues (1-2 per project)
- Mostly target framework updates
- Few or no package updates
- Straightforward changes

**Effort Factors**:
- Issue volume: Low
- Code changes: Minimal
- Testing: Low (basic functionality)
- Risk: Low

**Dependency Ordering Impact**: **None** (all independent projects)

**Bottom-Up Strategy Application**:
- Aggressive batching (10-15 projects per batch)
- Maximum parallelization opportunity
- Minimal effort per project

**Batching Efficiency**:
- Highest batch sizes in entire upgrade
- Repetitive, straightforward process
- Quick validation per project

#### Phase 6: Test Projects

**Complexity**: **Low**  
**Rationale**:
- Depend on already-upgraded Tier 0 projects
- Test framework updates straightforward
- Minimal code changes expected

**Effort Factors**:
- Package updates: Low
- Test framework compatibility: Low
- Dependency alignment: Low (dependencies already on .NET 10.0)
- Validation: Low

**Dependency Ordering Impact**: **Medium** (depends on 3 Tier 0 projects)

**Bottom-Up Strategy Application**:
- Process all 3 together after Tier 0 complete
- Dependencies stable and validated
- No multi-targeting needed

### Resource Requirements

#### Skills Required

**Phase 1-2 (Critical/High Priority)**:
- **Deep .NET expertise**: Understanding API changes, migration patterns
- **Architectural knowledge**: Package replacement decisions
- **Problem-solving**: Research and workaround development
- **Testing expertise**: Comprehensive validation required

**Phase 3 (High Complexity)**:
- **Strong .NET knowledge**: Source incompatible API resolution
- **Domain expertise**: EF Core, Identity, authentication
- **Systematic approach**: Pattern identification and replication
- **Testing discipline**: Thorough validation per batch

**Phase 4-5 (Medium/Low Complexity)**:
- **Solid .NET knowledge**: Standard upgrade tasks
- **Efficiency focus**: Batch processing, pattern application
- **Quality assurance**: Spot-check validation

**Phase 6 (Test Projects)**:
- **Testing expertise**: Test framework updates
- **Dependency management**: Version alignment

#### Parallel Processing Capacity

**Realistic Constraints**:
- Build system: Single-threaded (Visual Studio/MSBuild)
- File locking: Project file edits sequential
- Testing: Can run in parallel if test runner supports

**Effective Parallelization**:
- **Logical batching**: Group projects conceptually
- **Sequential execution**: Process batches one at a time
- **Concurrent research**: Team can research multiple projects simultaneously
- **Distributed work**: Different team members on different batches

**Team Size Considerations**:

**Single Developer**:
- Process phases sequentially
- Focus on one batch at a time
- Leverage patterns from earlier batches
- Estimated timeline: **Medium** (dependent on project complexity)

**Small Team (2-3 developers)**:
- Can parallelize within phases
- One dev on high-risk, others on batches
- Shared knowledge transfer
- Estimated timeline: **Medium** (efficiency gains from parallelization)

**Larger Team (4+ developers)**:
- Significant parallelization within phases
- Multiple batches simultaneously
- Dedicated roles (upgrader, tester, documenter)
- Estimated timeline: **Medium** (diminishing returns, coordination overhead)

### Relative Complexity Ratings

**Project-Level Complexity** (1-5 scale):

| Rating | Criteria | Example Projects | Project Count |
|--------|----------|-----------------|---------------|
| **5 - Very High** | Binary incompatible APIs, major code changes | StoreViewerApplication, DesigningForAutomaticBinding | 4 |
| **4 - High** | Deprecated packages, 15+ issues, complex scenarios | FluentValidation, Quartz projects, Identity projects | 8 |
| **3 - Medium** | 10-50 issues, source incompatible APIs, EF Core | RecipeApplication variants, ConsoleClients | 52 |
| **2 - Low-Medium** | 3-9 issues, behavioral changes, moderate packages | Web APIs, Razor Pages, Background services | 32 |
| **1 - Low** | 1-2 issues, framework update only | Minimal APIs, simple examples | 27 |

**Phase-Level Complexity** (1-5 scale):

| Phase | Complexity | Project Count | Effort per Project | Total Effort |
|-------|-----------|---------------|-------------------|--------------|
| **Phase 1** | 5 | 4 | High | High |
| **Phase 2** | 4 | 4 | High | High |
| **Phase 3** | 3-4 | 28 | Medium-High | High (volume) |
| **Phase 4** | 2-3 | 32 | Medium | Medium (volume) |
| **Phase 5** | 1-2 | 53 | Low | Medium (volume) |
| **Phase 6** | 1 | 3 | Low | Low |

### Effort Distribution Estimate

**Relative Effort by Phase** (percentage of total effort):

- **Phase 1** (4 projects): **20%** - High complexity, critical issues, pattern establishment
- **Phase 2** (4 projects): **15%** - Deprecated packages, research required
- **Phase 3** (28 projects): **30%** - High volume, medium-high complexity
- **Phase 4** (32 projects): **20%** - Medium volume and complexity
- **Phase 5** (53 projects): **12%** - High volume, low complexity
- **Phase 6** (3 projects): **3%** - Low complexity, dependencies stable

**Cumulative Progress**:
- After Phase 1: 3% projects complete, 20% effort expended
- After Phase 2: 7% projects complete, 35% effort expended
- After Phase 3: 30% projects complete, 65% effort expended
- After Phase 4: 57% projects complete, 85% effort expended
- After Phase 5: 100% Tier 0 complete, 97% effort expended
- After Phase 6: 100% complete

### Bottom-Up Strategy Efficiency Gains

**Pattern Replication**:
- **Phase 1**: Establishes API migration patterns → Reduces effort in later phases
- **Phase 2**: Establishes package migration patterns → Applies to similar scenarios
- **Phase 3**: Batch 1 establishes patterns → Batches 2-6 benefit (30-40% effort reduction per subsequent batch)
- **Phase 4-5**: Apply established patterns → Minimal discovery effort

**Dependency Ordering Benefits**:
- **No multi-targeting**: All Tier 0 independent → No version conflict resolution
- **Stable foundation**: Tier 0 complete before Tier 1 → Test projects upgrade smoothly
- **Clean validation**: Each tier validated before next → No regression across tiers

**Incremental Validation**:
- Each phase self-contained → Issues isolated, no cross-contamination
- Early phases inform later phases → Learning curve benefits
- Low-risk phases later → Confidence built progressively

### Critical Path Analysis

**Longest Path**: Sequential processing of all phases  
**Parallelization Opportunities**: Within-phase batching in Phases 3-5  
**Bottlenecks**: Phase 1 (critical issues block progress), Phase 3 Batch 1 (pattern establishment)  
**Acceleration Options**: Increase batch sizes in Phases 4-5 after patterns established

---

## Source Control Strategy

### Branch Strategy

**Upgrade Branch**: `upgrade-to-NET10` (already created)  
**Source Branch**: `main`  
**Strategy**: Feature branch workflow with phase-level commits  

#### Branch Structure

```
main (stable, .NET 7.0)
  └─ upgrade-to-NET10 (upgrade work)
       ├─ phase-1-binary-incompatible (optional sub-branch)
       ├─ phase-2-deprecated-packages (optional sub-branch)
       └─ ... (optional sub-branches per phase)
```

**Recommended Approach**: Work directly on `upgrade-to-NET10` branch, use commits to mark phase boundaries.

**Alternative Approach** (for larger teams): Create sub-branches per phase, merge to `upgrade-to-NET10` after phase validation.

---

### Commit Strategy

#### Commit Frequency

**Phase-Level Commits** (Minimum):
- Commit after each phase completes and validates
- Phase commit = all projects in phase upgraded

**Project-Level Commits** (Optional, for high-risk projects):
- Commit after each Phase 1-2 project completes
- Provides finer-grained rollback points

**Batch-Level Commits** (Recommended for Phase 3-5):
- Commit after each batch within phase completes
- Balances granularity and commit volume

#### Commit Message Format

Follow conventional commit format:

```
<type>(<scope>): <subject>

<body>

<footer>
```

**Types**:
- `upgrade`: Target framework or package upgrade
- `fix`: Breaking change fix, API migration
- `refactor`: Code changes for .NET 10.0 compatibility
- `test`: Test updates for .NET 10.0
- `docs`: Documentation updates

**Examples**:

```
upgrade(phase1): Migrate StoreViewerApplication to .NET 10.0

- Update target framework to net10.0
- Replace Configure<T>(IConfiguration) with BindConfiguration
- Test configuration binding
- Validate application runs correctly

Closes: Phase 1, Project 1.1
```

```
upgrade(phase2): Migrate FluentValidationConverter to .NET 10.0

- Update target framework to net10.0
- Remove deprecated FluentValidation.AspNetCore
- Add FluentValidation base package
- Update validation registration
- Test validator execution

Deprecated package: FluentValidation.AspNetCore removed
Replacement: FluentValidation 11.9.0 + DI extensions

Closes: Phase 2, Project 2.1
```

```
upgrade(phase3-batch3a): Migrate Identity projects to .NET 10.0

Projects upgraded:
- Chapter23/A_DefaultTemplate_LocalDB
- Chapter23/B_DefaultTemplate_SQLite
- Chapter23/D_RecipeApp_Identity_SQLite
- Chapter23/E_RecipeApp_NameClaim_SQLite
- Chapter24/A_Airport
- Chapter24/B_RecipeApp_Identity_SQLite

Changes:
- Update target frameworks to net10.0
- Update Microsoft.AspNetCore.Identity.* packages to 10.0.3
- Update EF Core packages to 10.0.3
- Fix source incompatible APIs
- Test authentication flows

Closes: Phase 3, Batch 3A
```

---

### Commit Checkpoints

**Phase 1 Commits** (4 commits):
```
upgrade(phase1): Migrate StoreViewerApplication to .NET 10.0
upgrade(phase1): Migrate DesigningForAutomaticBinding to .NET 10.0
upgrade(phase1): Migrate UsingDifferentEnvironments to .NET 10.0
upgrade(phase1): Migrate ConfigureOptionsExample to .NET 10.0
```

**Phase 2 Commits** (4-5 commits):
```
upgrade(phase2): Migrate FluentValidationConverter to .NET 10.0
upgrade(phase2): Migrate SystemdService to .NET 10.0
upgrade(phase2): Migrate WindowsService to .NET 10.0
upgrade(phase2): Migrate QuartzHostedService to .NET 10.0
upgrade(phase2): Migrate QuartzClustering to .NET 10.0
```

**Phase 3 Commits** (6 commits - one per batch):
```
upgrade(phase3-batch3a): Migrate Identity authentication projects (6 projects)
upgrade(phase3-batch3b): Migrate Recipe API projects (3 projects)
upgrade(phase3-batch3c): Migrate RecipeApplication EF Core projects (5 projects)
upgrade(phase3-batch3d): Migrate RecipeApplication feature projects (5 projects)
upgrade(phase3-batch3e): Migrate Identity RecipeApplication projects (6 projects)
upgrade(phase3-batch3f): Migrate ConsoleClient and ExchangeRateViewer projects (4 projects)
```

**Phase 4 Commits** (5 commits - one per batch):
```
upgrade(phase4-batch4a): Migrate Web API projects (7 projects)
upgrade(phase4-batch4b): Migrate Tag Helpers & Razor Pages (8 projects)
upgrade(phase4-batch4c): Migrate Background services (2 projects)
upgrade(phase4-batch4d): Migrate Security & CORS examples (5 projects)
upgrade(phase4-batch4e): Migrate miscellaneous ASP.NET Core projects (10 projects)
```

**Phase 5 Commits** (5 commits - one per batch):
```
upgrade(phase5-batch5a): Migrate Minimal API examples (13 projects)
upgrade(phase5-batch5b): Migrate Routing & Model Binding examples (5 projects)
upgrade(phase5-batch5c): Migrate Dependency Injection examples (6 projects)
upgrade(phase5-batch5d): Migrate Configuration examples (12 projects)
upgrade(phase5-batch5e): Migrate Razor Pages basics (17 projects)
```

**Phase 6 Commit** (1 commit):
```
upgrade(phase6): Migrate test projects to .NET 10.0 (3 projects)
```

**Tier Completion Commits** (2 major milestones):
```
milestone(tier0): Complete Tier 0 upgrade - all 117 independent projects on .NET 10.0
milestone(tier1): Complete Tier 1 upgrade - all 3 test projects on .NET 10.0
```

**Total Commits**: ~25-30 commits

---

### Review and Merge Process

#### Pull Request Strategy

**Option A: Single Large PR** (Simpler):
- Create one PR: `upgrade-to-NET10` → `main`
- Review entire upgrade at once
- Pros: Simple, single merge
- Cons: Large PR, difficult to review

**Option B: Phase-Based PRs** (Recommended):
- Create PR after each major phase or tier
- PR 1: Phase 1-2 complete (critical/high-priority)
- PR 2: Phase 3 complete (high-complexity)
- PR 3: Phase 4-5 complete (medium/low-complexity)
- PR 4: Phase 6 complete (test projects)
- Pros: Incremental review, early feedback, manageable PR size
- Cons: More coordination, potential merge conflicts

**Option C: Tier-Based PRs** (Balanced):
- PR 1: Tier 0 complete (all 117 projects)
- PR 2: Tier 1 complete (all 3 test projects)
- Pros: Natural breakpoints, validated tiers
- Cons: Large first PR

**Recommendation**: Use **Option B (Phase-Based PRs)** for manageable review sizes.

#### Pull Request Checklist

For each PR:

**Before Creating PR**:
- [ ] All projects in scope build successfully
- [ ] All tests pass (or failures documented)
- [ ] No security vulnerabilities
- [ ] Breaking changes documented
- [ ] Commit history clean and meaningful
- [ ] Branch up-to-date with source branch

**PR Description Template**:
```markdown
## .NET 10.0 Upgrade - [Phase/Tier Name]

### Scope
- Projects upgraded: [count]
- Phases covered: [list]

### Changes
- Target framework: net7.0 → net10.0
- Packages updated: [list major packages]
- Breaking changes addressed: [list]
- Deprecated packages: [list actions taken]

### Testing
- Build: ✅ All projects build successfully
- Tests: ✅ [X/Y] tests passing ([Y-X] documented failures)
- Smoke tests: ✅ Representative projects validated

### Breaking Changes
[List all breaking changes and resolutions]

### Migration Patterns
[Document patterns established for reuse]

### Risks
[Note any remaining risks or deferred issues]

### Validation Checklist
- [ ] Full solution builds
- [ ] Tests pass
- [ ] No security vulnerabilities
- [ ] Documentation updated
- [ ] Commit history clean
```

**PR Review Process**:
1. **Automated Checks**: CI/CD pipeline runs (build, test)
2. **Code Review**: Team reviews changes, patterns, decisions
3. **Testing Validation**: Reviewer verifies testing completed
4. **Documentation Review**: Ensure patterns and decisions documented
5. **Approval**: 1-2 approvers required
6. **Merge**: Squash or merge commits to target branch

---

### Merge Strategy

**For Phase-Based PRs**:
- **Merge Commit**: Preserve phase structure in history
- **Tag**: Tag each merged phase (optional)

**For Final Merge to `main`**:
- **Merge Commit**: Preserve full upgrade history
- **Tag**: `v3.0-net10.0` or similar version tag
- **Release Notes**: Document upgrade completion

**Merge Commands**:
```bash
# Merge phase PR (preserve history)
git checkout upgrade-to-NET10
git merge --no-ff phase-1-complete -m "Merge Phase 1: Binary incompatible APIs"

# Merge upgrade branch to main (final)
git checkout main
git merge --no-ff upgrade-to-NET10 -m "Upgrade solution to .NET 10.0"
git tag -a v3.0-net10.0 -m "ASP.NET Core in Action 3e - .NET 10.0"
git push origin main --tags
```

---

### Conflict Resolution

**Expected Conflicts**:
- `.csproj` files (target framework, package versions)
- `Program.cs` files (API changes)
- `appsettings.json` (configuration structure changes)

**Resolution Strategy**:
1. **Favor upgrade branch**: For project files and package versions
2. **Review carefully**: For code files with API changes
3. **Test after resolution**: Build and test after resolving conflicts

**Conflict Prevention**:
- Keep `upgrade-to-NET10` branch updated with `main` periodically
- Communicate with team about simultaneous work
- Use feature flags if needed for gradual rollout

---

### Backup and Safety

**Before Starting Upgrade**:
```bash
# Create backup branch
git checkout main
git branch backup-pre-net10-upgrade
git push origin backup-pre-net10-upgrade
```

**Before Each Major Phase**:
```bash
# Tag current state
git tag phase-1-start
git tag phase-2-start
# ... etc.
```

**Rollback Scenarios**:

**Rollback Single Commit**:
```bash
git revert <commit-hash>
```

**Rollback to Phase Start**:
```bash
git reset --hard phase-3-start
```

**Rollback Entire Upgrade** (nuclear option):
```bash
git checkout main
git branch -D upgrade-to-NET10
git checkout -b upgrade-to-NET10
# Start over
```

---

### Documentation Commits

**Commit Documentation Changes**:
- Commit this plan.md when finalized
- Commit updated README.md with .NET 10.0 requirements
- Commit any architecture decision records (ADRs)

```bash
git add plan.md
git commit -m "docs(upgrade): Add .NET 10.0 upgrade plan"

git add README.md
git commit -m "docs(readme): Update for .NET 10.0 requirements"
```

---

### Post-Merge Activities

**After Successful Merge to `main`**:

1. **Update CI/CD**: Ensure pipelines target .NET 10.0 SDK
2. **Update Documentation**: README, wiki, contributor guides
3. **Notify Team**: Announce upgrade completion
4. **Archive Branch**: Keep `upgrade-to-NET10` for reference
5. **Clean Up**: Delete temporary/backup branches
6. **Monitor**: Watch for issues in production/staging

**Cleanup Commands**:
```bash
# Archive upgrade branch (don't delete immediately)
git tag archive/upgrade-to-NET10 upgrade-to-NET10
git push origin archive/upgrade-to-NET10

# After 30 days, can delete branch
git branch -d upgrade-to-NET10
git push origin --delete upgrade-to-NET10
```

---

### Bottom-Up Strategy Source Control Alignment

**Tier-Based Commits**:
- Tier 0 completion = major milestone commit
- Tier 1 completion = final milestone commit

**Dependency Ordering Preserved**:
- Commits flow from Tier 0 → Tier 1
- History shows dependency-first approach
- Tags mark tier boundaries

**Validation Checkpoints**:
- Each tier completion commit includes full validation
- No proceeding to next tier without validated commit
- Clean, reviewable history per tier

**Benefits**:
- Clear progression through dependency tiers
- Easy to identify when foundation (Tier 0) stabilized
- Natural rollback points at tier boundaries
- History reflects upgrade strategy

---

## Success Criteria

### Technical Criteria

The upgrade is considered successful when ALL of the following technical criteria are met:

#### 1. Target Framework Migration

- [x] **All 120 projects target .NET 10.0**
  - 117 Tier 0 projects: `<TargetFramework>net10.0</TargetFramework>`
  - 3 Tier 1 projects: `<TargetFramework>net10.0</TargetFramework>`
  - 0 projects remain on net7.0
  - Verification: `grep -r "net7.0" **/*.csproj` returns 0 results (exclude comments)

#### 2. Package Updates

- [x] **All .NET packages updated to 10.0.x versions**
  - Microsoft.AspNetCore.* packages: 7.0.0 → 10.0.3
  - Microsoft.EntityFrameworkCore.* packages: 7.0.0 → 10.0.3
  - Microsoft.Extensions.* packages: 7.0.0 → 10.0.3
  - Microsoft.AspNetCore.Identity.* packages: 7.0.0 → 10.0.3
  - Microsoft.AspNetCore.Authentication.* packages: 7.0.0 → 10.0.3
  - Microsoft.AspNetCore.Mvc.* packages: 7.0.0 → 10.0.3
  - Microsoft.AspNetCore.OpenApi: 7.0.0 → 10.0.3
  - Microsoft.NET.Test.Sdk remains compatible (17.3.2 or later)
  - Microsoft.VisualStudio.Web.CodeGeneration.Design: 7.0.0 → 10.0.2
  - Newtonsoft.Json: 13.0.1 → 13.0.4
  - Microsoft.Net.Http.Headers: 2.2.8 → 10.0.3

- [x] **All package updates applied**
  - 23 packages from assessment updated
  - No packages remain on .NET 7.0 versions
  - Compatible third-party packages validated (Quartz, Swashbuckle, NSwag, etc.)

- [x] **Deprecated packages addressed**
  - FluentValidation.AspNetCore: Removed and replaced with base FluentValidation package
  - Microsoft.Extensions.Hosting.Systemd: Updated to 10.0.3 (deprecated but functional)
  - Microsoft.Extensions.Hosting.WindowsServices: Updated to 10.0.3 (deprecated but functional)
  - Quartz hosting extensions: Verified compatible, updated if available

#### 3. Build Success

- [x] **Full solution builds without errors**
  - Command: `dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release`
  - Exit code: 0
  - Compilation errors: 0
  - All 120 projects compile successfully

- [x] **Individual project builds succeed**
  - Each project can build in isolation
  - No inter-project build dependencies broken
  - NuGet restore succeeds for all projects

- [x] **Warnings acceptable**
  - No new critical warnings introduced
  - Warning count increase < 5% (if any)
  - All warnings reviewed and documented

#### 4. Breaking Changes Resolved

- [x] **Binary incompatible APIs fixed** (4 projects)
  - `Configure<T>(IServiceCollection, IConfiguration)` replaced with `BindConfiguration`
  - All affected projects compile successfully
  - Configuration binding works correctly
  - Options pattern functional

- [x] **Source incompatible APIs addressed** (317 occurrences)
  - Obsolete API calls replaced
  - Method signature changes updated
  - Namespace changes applied
  - All source incompatibilities resolved

- [x] **Behavioral changes validated** (216 occurrences)
  - Application behavior reviewed
  - Undesirable changes mitigated
  - Expected changes documented
  - No regressions detected

#### 5. Testing

- [x] **Test projects build successfully** (3 projects)
  - Chapter35\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests
  - Chapter36\A_ExchangeRatesWeb\test\ExchangeRates.Web.Tests
  - Chapter36\B_RecipeApplication_SQLite\test\RecipeApplication.Tests

- [x] **Test suites execute**
  - All test projects run without infrastructure errors
  - Test frameworks compatible with .NET 10.0
  - Test runners functional

- [x] **Test results acceptable**
  - ≥ 95% of tests pass
  - Failures documented and justified
  - No unexpected test failures
  - Behavioral changes reflected in tests

#### 6. Security

- [x] **No security vulnerabilities**
  - Command: `dotnet list package --vulnerable` returns no vulnerabilities
  - No CVEs introduced
  - No insecure packages added
  - Deprecated packages evaluated for security implications

- [x] **Security features functional**
  - Identity/authentication projects work correctly
  - JWT authentication functional (if applicable)
  - Authorization policies work
  - CORS configuration correct
  - CSRF protection enabled (if applicable)

#### 7. Functional Validation

- [x] **Representative projects validated**
  - 10-15 projects from different categories tested
  - ASP.NET Core web applications run
  - Web APIs respond correctly
  - Blazor WebAssembly projects load
  - Worker Services execute tasks
  - Console applications run
  - Background services function

- [x] **Critical functionality verified**
  - Configuration loading works
  - Dependency injection functional
  - Database operations succeed (EF Core projects)
  - Authentication/authorization works (Identity projects)
  - Middleware pipeline correct
  - Static file serving works
  - Exception handling functional

---

### Quality Criteria

The upgrade maintains quality standards:

#### 8. Code Quality

- [x] **Code quality maintained**
  - No code smells introduced by migration
  - API usage follows .NET 10.0 best practices
  - Configuration patterns idiomatic
  - Dependency injection patterns correct
  - Async/await patterns appropriate

- [x] **Project structure preserved**
  - No unnecessary structural changes
  - Folder organization maintained
  - Naming conventions consistent
  - Project dependencies unchanged (except framework version)

#### 9. Documentation

- [x] **Migration documented**
  - This plan.md complete and accurate
  - Breaking changes cataloged
  - API migrations documented
  - Package decisions justified
  - Patterns recorded

- [x] **Code documentation updated**
  - README.md reflects .NET 10.0 requirements
  - Build instructions updated
  - Prerequisites documented (.NET 10.0 SDK)
  - Known issues documented (if any)

- [x] **Lessons learned captured**
  - Patterns identified and documented
  - Common issues and resolutions recorded
  - Tips for similar upgrades captured

---

### Process Criteria

The upgrade followed the planned process:

#### 10. Bottom-Up Strategy Compliance

- [x] **Tier 0 completed first**
  - All 117 independent projects upgraded before Tier 1
  - Tier 0 validated before proceeding to Tier 1
  - No Tier 1 projects upgraded before dependencies

- [x] **Tier 1 completed second**
  - All 3 test projects upgraded after their dependencies
  - Test projects depend on stable Tier 0 projects
  - No multi-targeting required

- [x] **Dependency order respected**
  - Projects upgraded in correct dependency order
  - No violations of Bottom-Up strategy principles
  - Test projects upgraded after application projects

#### 11. Phase Execution

- [x] **All 6 phases completed**
  - Phase 1: Binary incompatible APIs (4 projects) ✅
  - Phase 2: Deprecated packages (4 projects) ✅
  - Phase 3: High complexity (28 projects) ✅
  - Phase 4: Medium complexity (32 projects) ✅
  - Phase 5: Low complexity (53 projects) ✅
  - Phase 6: Test projects (3 projects) ✅

- [x] **Phase transitions validated**
  - Each phase validated before proceeding to next
  - Phase completion criteria met
  - No skipped phases or projects

#### 12. Source Control

- [x] **Commits follow strategy**
  - Phase-level commits created
  - Commit messages follow format
  - Meaningful commit history
  - Major milestones tagged

- [x] **Branch strategy followed**
  - Work completed on `upgrade-to-NET10` branch
  - Source control strategy documented and followed
  - Pull requests created and reviewed (if applicable)
  - Merge completed to `main` branch

---

### Acceptance Criteria

The upgrade is **COMPLETE** and ready for release when:

✅ **All 120 projects target .NET 10.0**  
✅ **All required packages updated to .NET 10.0 versions**  
✅ **Full solution builds successfully (0 errors)**  
✅ **All breaking changes resolved and functional**  
✅ **Test projects build and execute**  
✅ **No security vulnerabilities present**  
✅ **Representative projects validated and functional**  
✅ **Code quality maintained**  
✅ **Documentation updated and complete**  
✅ **Bottom-Up strategy followed (Tier 0 → Tier 1)**  
✅ **All 6 phases completed and validated**  
✅ **Source control strategy followed, changes committed**

---

### Verification Commands

**Final Verification Checklist**:

```bash
# 1. Verify all projects target net10.0
grep -r "<TargetFramework>net7.0</TargetFramework>" . --include="*.csproj"
# Expected: No results

# 2. Full solution build
dotnet build asp-dot-net-core-in-action-3e.sln --configuration Release
# Expected: Build succeeded. 0 Error(s)

# 3. Check for security vulnerabilities
dotnet list package --vulnerable
# Expected: No vulnerable packages

# 4. Check for deprecated packages
dotnet list package --deprecated
# Expected: Only documented deprecated packages (hosting extensions)

# 5. Run all tests
dotnet test asp-dot-net-core-in-action-3e.sln --configuration Release
# Expected: ≥ 95% pass rate

# 6. Verify package versions (sample)
dotnet list package | grep "Microsoft.AspNetCore"
# Expected: All show version 10.0.x

dotnet list package | grep "Microsoft.EntityFrameworkCore"
# Expected: All show version 10.0.x

# 7. Count projects by target framework
grep -r "<TargetFramework>" . --include="*.csproj" | grep "net10.0" | wc -l
# Expected: 120

# 8. Verify git branch and commits
git branch
# Expected: * upgrade-to-NET10

git log --oneline --graph --decorate | head -30
# Expected: See phase commits, milestones

# 9. Check for uncommitted changes
git status
# Expected: Clean working directory (or only plan.md)

# 10. Validate project count
find . -name "*.csproj" | wc -l
# Expected: 124 (includes duplicate projects in solution)
```

---

### Sign-Off

**Upgrade Complete**: ✅ / ❌

**Completed By**: _______________  
**Completion Date**: _______________  
**Reviewed By**: _______________  
**Review Date**: _______________  

**Notes**:
- Outstanding Issues: _______________
- Deferred Items: _______________
- Follow-Up Actions: _______________

---

### Post-Upgrade Actions

After sign-off, complete these actions:

1. **Merge to main branch**
   ```bash
   git checkout main
   git merge --no-ff upgrade-to-NET10
   git tag -a v3.0-net10.0 -m "ASP.NET Core in Action 3e - .NET 10.0"
   git push origin main --tags
   ```

2. **Update CI/CD pipelines**
   - Update build agents to use .NET 10.0 SDK
   - Update Docker images to .NET 10.0 base images
   - Update deployment scripts for .NET 10.0 runtime

3. **Update documentation**
   - README.md: .NET 10.0 requirements
   - CONTRIBUTING.md: Build instructions for .NET 10.0
   - Wiki: Migration notes and lessons learned

4. **Notify stakeholders**
   - Announce upgrade completion
   - Share release notes
   - Document breaking changes for consumers

5. **Monitor production/staging**
   - Watch for runtime issues
   - Monitor performance metrics
   - Collect feedback
   - Address issues promptly

6. **Archive upgrade artifacts**
   - Keep upgrade branch for reference
   - Save migration documentation
   - Preserve lessons learned
   - Update project history

---

**Upgrade Completion**: This upgrade is complete when all criteria above are met and signed off.

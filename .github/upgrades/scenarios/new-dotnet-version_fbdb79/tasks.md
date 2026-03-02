# ASP.NET Core in Action 3e .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of upgrading ASP.NET Core in Action 3rd Edition solution from .NET 7.0 to .NET 10.0 (LTS). The solution contains 120 projects organized by chapter, which will be upgraded incrementally following a bottom-up, dependency-first approach across two tiers.

**Progress**: 7/24 tasks complete (29%) ![0%](https://progress-bar.xyz/29)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2026-03-02 03:02)*
**References**: Plan §Executive Summary, Plan §Migration Strategy

- [✓] (1) Verify .NET 10.0 SDK installed on development machine
- [✓] (2) SDK version 10.0.x detected (**Verify**)
- [✓] (3) Check global.json file compatibility with .NET 10.0 SDK (if present in repository)
- [✓] (4) Global.json compatible or absent (**Verify**)

---

### [✓] TASK-002: Upgrade Phase 1 projects with binary incompatible APIs *(Completed: 2026-03-02 03:15)*
**References**: Plan §Phase 1, Plan §Project-by-Project Plans §Phase 1

- [✓] (1) Update target framework to net10.0 for all 4 Phase 1 projects per Plan §Phase 1 (StoreViewerApplication, DesigningForAutomaticBinding, UsingDifferentEnvironments, ConfigureOptionsExample)
- [✓] (2) All Phase 1 project files updated to net10.0 (**Verify**)
- [✓] (3) Replace Configure<T>(IServiceCollection, IConfiguration) calls with BindConfiguration pattern in all affected files per Plan §Project 1.1-1.4 (Program.cs files, ~10 occurrences total across 4 projects)
- [✓] (4) All Configure<T> API calls replaced (**Verify**)
- [✓] (5) Build all Phase 1 projects
- [✓] (6) All Phase 1 projects build with 0 errors (**Verify**)
- [✓] (7) Test configuration binding in representative project (StoreViewerApplication)
- [✓] (8) Configuration values load correctly from appsettings.json (**Verify**)
- [✓] (9) Commit changes with message: "TASK-002: Upgrade Phase 1 projects (binary incompatible APIs) to .NET 10.0"

---

### [✓] TASK-003: Upgrade Phase 2 projects with deprecated packages *(Completed: 2026-03-02 03:18)*
**References**: Plan §Phase 2, Plan §Project-by-Project Plans §Phase 2

- [✓] (1) Update target framework to net10.0 for all 4 Phase 2 projects per Plan §Phase 2 (FluentValidationConverter, SystemdService, WindowsService, QuartzHostedService, QuartzClustering)
- [✓] (2) All Phase 2 project files updated to net10.0 (**Verify**)
- [✓] (3) Update Microsoft.Extensions.Hosting packages to 10.0.3 per Plan §Phase 2
- [✓] (4) Handle FluentValidation.AspNetCore deprecation per Plan §Project 2.1 (remove deprecated package, add FluentValidation 11.9.0 and DI extensions, update validation registration in Program.cs)
- [✓] (5) Update Quartz packages and verify compatibility per Plan §Projects 2.4-2.5
- [✓] (6) All packages updated (**Verify**)
- [✓] (7) Build all Phase 2 projects
- [✓] (8) All Phase 2 projects build with 0 errors (**Verify**)
- [✓] (9) Test FluentValidation migration (run FluentValidationConverter project, verify validation executes)
- [✓] (10) Validation works correctly (**Verify**)
- [✓] (11) Commit changes with message: "TASK-003: Upgrade Phase 2 projects (deprecated packages) to .NET 10.0"

---

### [✓] TASK-004: Upgrade Phase 3 Batch 3A (Identity and authentication projects) *(Completed: 2026-03-02 03:22)*
**References**: Plan §Phase 3, Plan §Detailed Dependency Analysis §Tier 0 §Group 0C

- [✓] (1) Update target framework to net10.0 for Batch 3A projects per Plan §Phase 3 Batch 3A (Chapter23-24 Identity/Auth projects, 6 projects)
- [✓] (2) Update Microsoft.AspNetCore.Identity.* packages to 10.0.3 per Plan §Success Criteria §Package Updates
- [✓] (3) Update Microsoft.EntityFrameworkCore.* packages to 10.0.3 per Plan §Success Criteria §Package Updates
- [✓] (4) Restore dependencies for Batch 3A
- [✓] (5) Build Batch 3A projects and fix compilation errors per Plan §Detailed Dependency Analysis (reference breaking changes catalog for Identity API changes)
- [✓] (6) All Batch 3A projects build with 0 errors (**Verify**)
- [✓] (7) Run representative Identity project, test authentication flow
- [✓] (8) Authentication works correctly (**Verify**)
- [✓] (9) Commit changes with message: "TASK-004: Upgrade Phase 3 Batch 3A (Identity/Auth projects) to .NET 10.0"

---

### [✓] TASK-005: Upgrade Phase 3 Batch 3B (Recipe API projects) *(Completed: 2026-03-02 03:33)*
**References**: Plan §Phase 3

- [✓] (1) Update target framework to net10.0 for Batch 3B projects per Plan §Phase 3 Batch 3B (Chapter25 Recipe API with Auth, 3 projects)
- [✓] (2) Update Microsoft.AspNetCore.* packages to 10.0.3
- [✓] (3) Update authentication-related packages to 10.0.3
- [✓] (4) Build Batch 3B projects and fix compilation errors
- [✓] (5) All Batch 3B projects build with 0 errors (**Verify**)
- [✓] (6) Test API endpoints (run one project, verify API responds)
- [✓] (7) API responds correctly (**Verify**)
- [✓] (8) Commit changes with message: "TASK-005: Upgrade Phase 3 Batch 3B (Recipe API projects) to .NET 10.0"

---

### [✓] TASK-006: Upgrade Phase 3 Batch 3C (RecipeApplication variants) *(Completed: 2026-03-02 03:39)*
**References**: Plan §Phase 3

- [✓] (1) Update target framework to net10.0 for Batch 3C projects per Plan §Phase 3 Batch 3C (Chapter12 RecipeApplication variants, 5 projects)
- [✓] (2) Update Microsoft.EntityFrameworkCore.* packages to 10.0.3
- [✓] (3) Update Microsoft.AspNetCore.* packages to 10.0.3
- [✓] (4) Build Batch 3C projects and fix compilation errors per Plan (EF Core API changes)
- [✓] (5) All Batch 3C projects build with 0 errors (**Verify**)
- [✓] (6) Test EF Core migrations (run migrations in one project if applicable)
- [✓] (7) Migrations execute successfully (**Verify**)
- [✓] (8) Commit changes with message: "TASK-006: Upgrade Phase 3 Batch 3C (RecipeApplication variants) to .NET 10.0"

---

### [✓] TASK-007: Upgrade Phase 3 Batch 3D (RecipeApplication with features) *(Completed: 2026-03-02 03:43)*
**References**: Plan §Phase 3

- [✓] (1) Update target framework to net10.0 for Batch 3D projects per Plan §Phase 3 Batch 3D (Chapter22, 26, 30, 32, 36 RecipeApplication features, 5 projects)
- [✓] (2) Update Microsoft.EntityFrameworkCore.* packages to 10.0.3
- [✓] (3) Update Microsoft.AspNetCore.* packages to 10.0.3
- [✓] (4) Update feature-specific packages (validation, configuration, custom middleware)
- [✓] (5) Build Batch 3D projects and fix compilation errors
- [✓] (6) All Batch 3D projects build with 0 errors (**Verify**)
- [✓] (7) Commit changes with message: "TASK-007: Upgrade Phase 3 Batch 3D (RecipeApplication features) to .NET 10.0"

---

### [▶] TASK-008: Upgrade Phase 3 Batch 3E (Identity RecipeApplication projects)
**References**: Plan §Phase 3

- [▶] (1) Update target framework to net10.0 for Batch 3E projects per Plan §Phase 3 Batch 3E (Chapter23-24 Identity RecipeApplication, 6 projects)
- [ ] (2) Update Microsoft.AspNetCore.Identity.* packages to 10.0.3
- [ ] (3) Update Microsoft.EntityFrameworkCore.* packages to 10.0.3
- [ ] (4) Build Batch 3E projects and fix compilation errors
- [ ] (5) All Batch 3E projects build with 0 errors (**Verify**)
- [ ] (6) Commit changes with message: "TASK-008: Upgrade Phase 3 Batch 3E (Identity RecipeApplication) to .NET 10.0"

---

### [ ] TASK-009: Upgrade Phase 3 Batch 3F (ConsoleClient and ExchangeRateViewer)
**References**: Plan §Phase 3

- [ ] (1) Update target framework to net10.0 for Batch 3F projects per Plan §Phase 3 Batch 3F (ConsoleClient projects, ExchangeRateViewer, 4 projects)
- [ ] (2) Update HTTP client packages (NSwag, Refit, or similar) to compatible versions
- [ ] (3) Update Microsoft.Extensions.* packages to 10.0.3
- [ ] (4) Build Batch 3F projects and fix compilation errors per Plan (HTTP client API changes)
- [ ] (5) All Batch 3F projects build with 0 errors (**Verify**)
- [ ] (6) Test API client generation (run one ConsoleClient project against corresponding API)
- [ ] (7) API client successfully calls endpoints (**Verify**)
- [ ] (8) Commit changes with message: "TASK-009: Upgrade Phase 3 Batch 3F (ConsoleClient and ExchangeRateViewer) to .NET 10.0"

---

### [ ] TASK-010: Upgrade Phase 4 Batch 4A (Web API projects)
**References**: Plan §Phase 4

- [ ] (1) Update target framework to net10.0 for Batch 4A projects per Plan §Phase 4 Batch 4A (Chapter11, 20 Web API projects, 7 projects)
- [ ] (2) Update Microsoft.AspNetCore.* packages to 10.0.3
- [ ] (3) Update Microsoft.AspNetCore.OpenApi to 10.0.3 (if used)
- [ ] (4) Update Swashbuckle or NSwag packages to compatible versions
- [ ] (5) Build Batch 4A projects and fix compilation errors
- [ ] (6) All Batch 4A projects build with 0 errors (**Verify**)
- [ ] (7) Test API functionality (run one project, verify endpoints respond)
- [ ] (8) API endpoints return expected responses (**Verify**)
- [ ] (9) Commit changes with message: "TASK-010: Upgrade Phase 4 Batch 4A (Web API projects) to .NET 10.0"

---

### [ ] TASK-011: Upgrade Phase 4 Batch 4B (Tag Helpers and Razor Pages)
**References**: Plan §Phase 4

- [ ] (1) Update target framework to net10.0 for Batch 4B projects per Plan §Phase 4 Batch 4B (Chapter16-18 Tag Helpers, Razor Pages, 8 projects)
- [ ] (2) Update Microsoft.AspNetCore.Mvc.* packages to 10.0.3
- [ ] (3) Build Batch 4B projects and fix compilation errors per Plan (Razor runtime compilation API changes)
- [ ] (4) All Batch 4B projects build with 0 errors (**Verify**)
- [ ] (5) Test Razor Pages rendering (run one project, verify page renders)
- [ ] (6) Razor Pages render correctly (**Verify**)
- [ ] (7) Commit changes with message: "TASK-011: Upgrade Phase 4 Batch 4B (Tag Helpers and Razor Pages) to .NET 10.0"

---

### [ ] TASK-012: Upgrade Phase 4 Batch 4C (Background services)
**References**: Plan §Phase 4

- [ ] (1) Update target framework to net10.0 for Batch 4C projects per Plan §Phase 4 Batch 4C (Chapter34 background services excluding deprecated hosting extensions, 2 projects)
- [ ] (2) Update Microsoft.Extensions.Hosting to 10.0.3
- [ ] (3) Build Batch 4C projects and fix compilation errors
- [ ] (4) All Batch 4C projects build with 0 errors (**Verify**)
- [ ] (5) Commit changes with message: "TASK-012: Upgrade Phase 4 Batch 4C (Background services) to .NET 10.0"

---

### [ ] TASK-013: Upgrade Phase 4 Batch 4D (Security and CORS examples)
**References**: Plan §Phase 4

- [ ] (1) Update target framework to net10.0 for Batch 4D projects per Plan §Phase 4 Batch 4D (Chapter28-29 Security, CORS, 5 projects)
- [ ] (2) Update Microsoft.AspNetCore.Authentication.* packages to 10.0.3
- [ ] (3) Update Microsoft.AspNetCore.Cors to 10.0.3
- [ ] (4) Build Batch 4D projects and fix compilation errors per Plan (authentication API changes)
- [ ] (5) All Batch 4D projects build with 0 errors (**Verify**)
- [ ] (6) Test authentication (run one project with auth, verify login works)
- [ ] (7) Authentication flow works correctly (**Verify**)
- [ ] (8) Commit changes with message: "TASK-013: Upgrade Phase 4 Batch 4D (Security and CORS) to .NET 10.0"

---

### [ ] TASK-014: Upgrade Phase 4 Batch 4E (Miscellaneous ASP.NET Core projects)
**References**: Plan §Phase 4

- [ ] (1) Update target framework to net10.0 for Batch 4E projects per Plan §Phase 4 Batch 4E (Chapter19, 22, 31-33 miscellaneous projects, 10 projects)
- [ ] (2) Update Microsoft.AspNetCore.* packages to 10.0.3
- [ ] (3) Update Microsoft.Extensions.* packages to 10.0.3
- [ ] (4) Build Batch 4E projects and fix compilation errors
- [ ] (5) All Batch 4E projects build with 0 errors (**Verify**)
- [ ] (6) Commit changes with message: "TASK-014: Upgrade Phase 4 Batch 4E (Miscellaneous projects) to .NET 10.0"

---

### [ ] TASK-015: Upgrade Phase 5 Batch 5A (Minimal API examples)
**References**: Plan §Phase 5

- [ ] (1) Update target framework to net10.0 for Batch 5A projects per Plan §Phase 5 Batch 5A (Chapter4-5 Minimal API examples, 13 projects)
- [ ] (2) Update Microsoft.AspNetCore.* packages to 10.0.3 (if any package references)
- [ ] (3) Build Batch 5A projects
- [ ] (4) All Batch 5A projects build with 0 errors (**Verify**)
- [ ] (5) Run one representative Minimal API project, verify endpoints respond
- [ ] (6) Minimal API endpoints work correctly (**Verify**)
- [ ] (7) Commit changes with message: "TASK-015: Upgrade Phase 5 Batch 5A (Minimal API examples) to .NET 10.0"

---

### [ ] TASK-016: Upgrade Phase 5 Batch 5B (Routing and Model Binding examples)
**References**: Plan §Phase 5

- [ ] (1) Update target framework to net10.0 for Batch 5B projects per Plan §Phase 5 Batch 5B (Chapter6-7, 14 routing, model binding, 5 projects)
- [ ] (2) Update Microsoft.AspNetCore.* packages to 10.0.3 (if any)
- [ ] (3) Build Batch 5B projects
- [ ] (4) All Batch 5B projects build with 0 errors (**Verify**)
- [ ] (5) Commit changes with message: "TASK-016: Upgrade Phase 5 Batch 5B (Routing and Model Binding) to .NET 10.0"

---

### [ ] TASK-017: Upgrade Phase 5 Batch 5C (Dependency Injection examples)
**References**: Plan §Phase 5

- [ ] (1) Update target framework to net10.0 for Batch 5C projects per Plan §Phase 5 Batch 5C (Chapter8-9 DI examples, 6 projects)
- [ ] (2) Update Microsoft.Extensions.DependencyInjection packages to 10.0.3 (if any)
- [ ] (3) Build Batch 5C projects
- [ ] (4) All Batch 5C projects build with 0 errors (**Verify**)
- [ ] (5) Commit changes with message: "TASK-017: Upgrade Phase 5 Batch 5C (Dependency Injection examples) to .NET 10.0"

---

### [ ] TASK-018: Upgrade Phase 5 Batch 5D (Configuration examples)
**References**: Plan §Phase 5

- [ ] (1) Update target framework to net10.0 for Batch 5D projects per Plan §Phase 5 Batch 5D (Chapter10, 13, 15 configuration examples, 12 projects)
- [ ] (2) Update Microsoft.Extensions.Configuration.* packages to 10.0.3 (if any)
- [ ] (3) Build Batch 5D projects
- [ ] (4) All Batch 5D projects build with 0 errors (**Verify**)
- [ ] (5) Commit changes with message: "TASK-018: Upgrade Phase 5 Batch 5D (Configuration examples) to .NET 10.0"

---

### [ ] TASK-019: Upgrade Phase 5 Batch 5E (Razor Pages basics)
**References**: Plan §Phase 5

- [ ] (1) Update target framework to net10.0 for Batch 5E projects per Plan §Phase 5 Batch 5E (Chapter13-15, 17 Razor Pages basics, 17 projects)
- [ ] (2) Update Microsoft.AspNetCore.Mvc.RazorPages to 10.0.3 (if explicit reference)
- [ ] (3) Build Batch 5E projects
- [ ] (4) All Batch 5E projects build with 0 errors (**Verify**)
- [ ] (5) Commit changes with message: "TASK-019: Upgrade Phase 5 Batch 5E (Razor Pages basics) to .NET 10.0"

---

### [ ] TASK-020: Validate Tier 0 completion
**References**: Plan §Testing & Validation Strategy §Level 3, Plan §Success Criteria

- [ ] (1) Build entire solution to verify all 117 Tier 0 projects compile successfully
- [ ] (2) Solution builds with 0 errors (**Verify**)
- [ ] (3) Run spot-check tests on 10-15 representative projects across all phases per Plan §Testing & Validation Strategy §Tier 0 Validation
- [ ] (4) Spot-check projects run successfully (**Verify**)
- [ ] (5) Verify no regressions by re-testing Phase 1-2 critical projects
- [ ] (6) Critical projects still work correctly (**Verify**)
- [ ] (7) Run package vulnerability scan on entire solution
- [ ] (8) No security vulnerabilities detected (**Verify**)

---

### [ ] TASK-021: Upgrade Phase 6 test projects (Tier 1)
**References**: Plan §Phase 6, Plan §Detailed Dependency Analysis §Tier 1

- [ ] (1) Update target framework to net10.0 for all 3 test projects per Plan §Phase 6 (ExchangeRates.Web.Tests in Chapter35 and Chapter36, RecipeApplication.Tests in Chapter36)
- [ ] (2) All test project files updated to net10.0 (**Verify**)
- [ ] (3) Update Microsoft.NET.Test.Sdk to 17.3.2 or later (if not already)
- [ ] (4) Update xUnit or NUnit packages to compatible versions (if applicable)
- [ ] (5) Restore test project dependencies
- [ ] (6) Build all 3 test projects
- [ ] (7) All test projects build with 0 errors (**Verify**)
- [ ] (8) Run all test suites
- [ ] (9) Fix any test failures (reference Plan §Breaking Changes for common issues)
- [ ] (10) Re-run tests after fixes
- [ ] (11) All tests pass with 0 failures (**Verify**)
- [ ] (12) Commit changes with message: "TASK-021: Upgrade Phase 6 test projects (Tier 1) to .NET 10.0"

---

### [ ] TASK-022: Final solution validation
**References**: Plan §Testing & Validation Strategy §Full Solution Testing, Plan §Success Criteria

- [ ] (1) Build complete solution to verify all 120 projects compile
- [ ] (2) Complete solution builds with 0 errors (**Verify**)
- [ ] (3) Run comprehensive smoke tests on representative projects from each category per Plan §Testing & Validation Strategy §Final Validation (Web apps, APIs, Blazor, Workers, Console apps)
- [ ] (4) All smoke tests pass (**Verify**)
- [ ] (5) Run complete package audit: dotnet list package --vulnerable
- [ ] (6) No security vulnerabilities detected (**Verify**)
- [ ] (7) Verify all deprecated packages documented and justified per Plan §Success Criteria §Package Updates
- [ ] (8) Deprecated packages documented (**Verify**)
- [ ] (9) Verify all .NET packages at 10.0.x versions per Plan §Success Criteria §Package Updates
- [ ] (10) Package versions correct (**Verify**)

---

### [ ] TASK-023: Final commit and documentation
**References**: Plan §Source Control Strategy, Plan §Success Criteria

- [ ] (1) Review all changes and ensure commit history is clean
- [ ] (2) Commit history reviewed (**Verify**)
- [ ] (3) Update README.md to reflect .NET 10.0 requirements
- [ ] (4) Update any architecture documentation with .NET 10.0 notes
- [ ] (5) Commit documentation changes with message: "TASK-023: Update documentation for .NET 10.0"
- [ ] (6) Create milestone tag: git tag -a v3.0-net10.0 -m "ASP.NET Core in Action 3e - .NET 10.0"
- [ ] (7) Tag created successfully (**Verify**)

---

### [ ] TASK-024: Prepare for merge to main branch
**References**: Plan §Source Control Strategy §Review and Merge Process

- [ ] (1) Ensure all 23 previous tasks completed successfully
- [ ] (2) All tasks marked complete (**Verify**)
- [ ] (3) Run final solution build as confirmation
- [ ] (4) Final build succeeds with 0 errors (**Verify**)
- [ ] (5) Create pull request from upgrade-to-NET10 to main branch per Plan §Source Control Strategy
- [ ] (6) Pull request includes comprehensive description of changes, breaking changes addressed, and testing completed
- [ ] (7) PR ready for review (**Verify**)

---

















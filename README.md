# FluentExtensions.Documents.Brazil 🇧🇷

FluentExtensions.Documents.Brazil provides **FluentValidation extensions** for validating **Brazilian documents**
using **official algorithms** and **state-specific rules**, with a clean, extensible, and production-ready architecture.

This library focuses on **correctness**, **clarity**, and **maintainability** — not regex-based shortcuts.

---

## Overview

Brazilian documents such as **CPF** and **CNPJ** follow well-defined but **non-uniform validation rules**.

This project provides:

- Algorithmic validation using **official specifications**
- FluentValidation-friendly APIs
- A design that scales as new rules and states are added

---

## Features

### Documents

- **CPF** — Cadastro de Pessoas Físicas
- **CNPJ** — Cadastro Nacional da Pessoa Jurídica
  - Numeric format
  - Alphanumeric format (future-ready – Receita Federal)
    
### Technical

- FluentValidation extension methods
- Shared normalization helpers
- Reusable Modulo-11 engine
- Clean Architecture friendly

---

## Installation

```bash
dotnet add package FluentExtensions.Documents.Brazil
```
## Usage

### CPF

Validates **Cadastro de Pessoas Físicas (CPF)** using the official **Modulo-11** algorithm.

```csharp
RuleFor(x => x.Cpf)
    .NotEmpty()
    .IsValidCpf();
```
### CNPJ

Validates **Cadastro Nacional da Pessoa Jurídica (CNPJ)** using the official **Modulo-11** algorithm.

```csharp
RuleFor(x => x.Cnpj)
    .NotEmpty()
    .IsValidCnpj();
```

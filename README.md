# Entra ID (Azure AD) User Fetcher

## Overview

This project is a .NET console application that connects to Microsoft Graph API using Azure AD (Microsoft Entra ID) to fetch user details securely.

## Features

- Connects to Microsoft Graph API using App (Application) permissions.
- Fetches user details like Display Name,Office Location, Mobile Number, and more.
- Uses `Azure.Identity` and `Microsoft.Graph` for secure authentication and API calls.

## Technologies

- .NET 8.0
- Azure.Identity (Authentication)
- Microsoft.Graph (Graph API integration)

## Prerequisites

- Azure AD App Registration (with Client ID, Tenant ID, Client Secret).
- Microsoft Graph API permissions (e.g., `User.Read.All`).
- .NET SDK installed.


This project is for learning and internal purposes.

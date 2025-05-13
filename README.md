<<<<<<< HEAD
﻿# Template App

This repository serves as an example of how to write plugins for the BlackBird platform.

## Actions

Actions represent specific tasks or operations that can be executed within our platform. To find the available actions and their implementation details, navigate to the `Actions` directory in the codebase.

In this directory, you will find file with list of actions.

## Webhooks

Webhooks enable the Template App to communicate with external services or applications in real-time. When specific events occur, our platform will send HTTP POST requests to the defined webhook endpoints. To view and manage the webhooks, go to the `Webhooks` directory in the codebase. You can also find `Handlers` and `Payloads` there. Handlers are used to manage webhook subscription when bird is created/deleted. Payloads represent webhook response data.

## Connections

Connections in the Template App are used to establish and manage connections to external services or APIs. They provide the necessary authentication and authorization details for interacting with these services securely. To explore existing connections or add new ones, head to the `Connections` directory and open the `ConnectionDefinition.cs` file.
In `OAuth` folder you can see OAuth management services. `OAuth2AuthorizeService` is responsible for building OAuth url for user authorization and `OAuth2TokenService` is used for managing user OAuth credentials.
=======
# Blackbird.io Appname

Blackbird is the new automation backbone for the language technology industry. Blackbird provides enterprise-scale automation and orchestration with a simple no-code/low-code platform. Blackbird enables ambitious organizations to identify, vet and automate as many processes as possible. Not just localization workflows, but any business and IT process. This repository represents an application that is deployable on Blackbird and usable inside the workflow editor.

## Introduction

<!-- begin docs -->

Documentation coming soon.

## Feedback

Do you want to use this app or do you have feedback on our implementation? Reach out to us using the [established channels](https://www.blackbird.io/) or create an issue.

<!-- end docs -->
>>>>>>> b0ee963a93c18628c011cd89125b3438d2eca223

# Shopping Online 

This module based on .Netcore , ef5 , Angular 8 

## Features

- login Module 
- Register Module
- Show All products filtered by category
- add item to cart
- purchase Item
- Admin Module
-- Manage Orders and update order status 
# Upcomming Improvement
- Add logger 
- add validations
- add loader indicator 
- improve purchaseing scenarios
- implement unit testing project
- CI/CD pipeline implementation

## Database Structure

| Table  | Description |

| ASPNetUsers | all users login system |

| ASPNetRoles | all permission roles |

| ASPNetUserRoles | role assigned for each user |

| Products | All products information |

| Categories | show product category |

| Cart | show carts assigned for each user |

| CartItems | products inside carts |

| CartStatus | status for cart ex:New,Checkedout,cancelled |

| Orders | order information for each user |

| OrderItems | product inside each order |

| OrdersStatus | status for orders ex:New,Checkedout,cancelled |

## Database Structure
this table will show all projects descriptions with dependencies

| Project  | dependency | Description
| ShoppingOnline.Data |  | contains database context for shopping online system
| ShoppingOnline.Domain.Model | | all models (tables) required to build DB.
| ShoppingOnline.Domain |ShoppingOnline.Domain.Model ,  ShoppingOnline.Data  | perform all shopping online business implementation
| ShoppingOnline.DTO |  | contains all data transfer object model to be passed to presentation layer (ShoppingOnline.Web)
| ShoppingOnline.API | ShoppingOnline.Data,ShoppingOnline.Domain  | contains all API consumed by presentation layer.
| ShoppingOnline.Web |  | presentation layer based on Angular8

## Presentation layer indeep
UI presentation based on Angular 8 and structured as following 
- [ ClientApp/Src/App ] -  cotains all component and views.
-- Admin : this section specified for admin component and views
- [ Directives ] - contains all directives used on app ex:"has-role" directive used for show elements to users with specific role.
- [ guards ] - contains all authentication guards required to handle routing.
- [ interceptors ] - http interceptor to inject token on headers for api authentication.
- [ modals ] - using bootstrap modals for popup windows.
- [ models ] - mapped dto classed to be presented on UI and pased to api controller.
- [ module ] - shared module contains all custom modules added.
- [ Services ] - contains all services consumed on controller to send http requests to API controller.

## API Controller Authentication
JWT token bearer authentication used to authenticate API controller with 2 roles (Admin,Member).

Any user register to app would be granter with "Member" role.

API authorized with 2 policies (RequireAdminRole,PurchaseOrder)

 

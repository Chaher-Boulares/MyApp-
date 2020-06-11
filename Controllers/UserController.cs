using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Infrastructure;
using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using Organization.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUser_EntityRepository _userEntityRepository;
        private readonly IEntityRepository _entityRepository;
       // private readonly IAffectedPermissionRepository _permissionRepository;
        private readonly IWallPermissionRepository _wallRepository;
        private readonly IOkrPermissionRepository _okrRepository;
        private readonly INotificationPermissionRepository _notificationRepository;
        private readonly IOrganizationPermissionRepository _organizationRepository;
        private readonly IChildEntityRepository _childRepository;
        private readonly OrganizationDBContext context;

        public UserController( OrganizationDBContext context , IChildEntityRepository childRepository, IOrganizationPermissionRepository organizationRepository, INotificationPermissionRepository notificationRepository, IOkrPermissionRepository okrRepository, IWallPermissionRepository wallRepository, IUserRepository userRepository, IUser_EntityRepository userEntityRepository, IEntityRepository entityRepository)
        {
            _userRepository = userRepository;
            _userEntityRepository = userEntityRepository;
            _entityRepository = entityRepository;
            //_permissionRepository = permissionRepository;
            _wallRepository = wallRepository;
            _okrRepository = okrRepository;
            _notificationRepository = notificationRepository;
            _organizationRepository = organizationRepository;
            _childRepository = childRepository;
            this.context = context;
        }


        // GET: api/[controller]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userRepository.GetAll();
        }


        // GET: api/[controller]/5
       // [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var movie = await _userRepository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
      //  [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            await _userRepository.Update(movie);
            return NoContent();
        }

        // POST: api/[controller]
       // [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserViewModel input)
        {
            var use = new User(input.UserName, input.Email, input.UserId);
            await _userRepository.Add(use);
            return CreatedAtAction("Get", use);
        }

       // [Authorize]
        [HttpPost("InviteManager")]
        public async Task<ActionResult<User_Entity>> PostManager([FromBody] InviteViewModel input)
        {
            
            var user = _userEntityRepository.GetByUserId(input.InviterId, input.EntityId);
            var Newuser = _userEntityRepository.GetByUserId(input.NewMemberId, input.EntityId);
            User New = await _userRepository.Get(input.NewMemberId);
            Entity ent = await _entityRepository.Get(input.EntityId);
            var userEntity = new User_Entity(New, ent, Role.Manager);
            Boolean admin = false;
                if (user.Role == Role.Manager)
                {
                    admin = true;
                }
                var childEntity = _childRepository.GetByParentId(input.EntityId);
                List<int> childs = new List<int>();
                foreach (ChildEntity ee in childEntity)
                {
                    int childId = ee.ChildId;
                    childs.Add(childId);
                }
            if (Newuser is null)    // sig que the new user is not a (member , manager or observer ) in this entity 
            {
                System.Diagnostics.Debug.WriteLine("the new user is not a (member , manager or observer ) in this entity  ");
                if (admin)
                {
                    if (input.NewRole == 0)    // we want to invite the new user as a manager to the entity 
                    {
                        foreach (int i in childs)   // we make the journey on all the  chids of the entity to make the newuser as a manager also at thoses entities  
                        {
                            Entity ii = await _entityRepository.Get(i);
                            var childuserEnt = new User_Entity(New, ii, Role.Manager);   //make the new user as a manager to the child af the entity 
                            var childwallPerm = new WallPermission(input.NewMemberId, i, true, true, true, true, true, true, true);
                            var childokrPerm = new OKRPermission(input.NewMemberId, i, true, true, true, true, true, true, true, true, true, true);
                            var childorgPerm = new OrganizationPermission(input.NewMemberId, i, true, true, true);
                            var childnotifPerm = new NotificationPermission(input.NewMemberId, i, true);
                            await _wallRepository.Add(childwallPerm);
                            await _okrRepository.Add(childokrPerm);
                            await _organizationRepository.Add(childorgPerm);
                            await _notificationRepository.Add(childnotifPerm);
                            await _userEntityRepository.Add(childuserEnt);

                        }
                        var wallPerm = new WallPermission(input.NewMemberId, input.EntityId, true, true, true, true, true, true, true);
                        var okrPerm = new OKRPermission(input.NewMemberId, input.EntityId, true, true, true, true, true, true, true, true, true, true);
                        var orgPerm = new OrganizationPermission(input.NewMemberId, input.EntityId, true, true, true);
                        var notifPerm = new NotificationPermission(input.NewMemberId, input.EntityId, true);
                        await _wallRepository.Add(wallPerm);
                        await _okrRepository.Add(okrPerm);
                        await _organizationRepository.Add(orgPerm);
                        await _notificationRepository.Add(notifPerm);
                        await _userEntityRepository.Add(userEntity);
                        Console.WriteLine("Creating Manager:" + New.UserId);
                    }
                }
                
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("the new user is  a (member , manager or observer ) in this entity  ");
                Newuser.Role = 0;
                System.Diagnostics.Debug.WriteLine(Newuser.Role);
                //await _userEntityRepository.Update(Newuser);
                User_Entity NewChange = await _userEntityRepository.Update(Newuser);
                var wallPerm1 = _wallRepository.GetByUserEntityId(input.NewMemberId, input.EntityId);
                var okrPer1 = _okrRepository.GetByUserEntityId(input.NewMemberId, input.EntityId);
                var orgPerm1 = _organizationRepository.GetOrgPermission(input.NewMemberId, input.EntityId);
                var notifPerm1 = _notificationRepository.GetByUserEntityId(input.NewMemberId, input.EntityId);
                System.Diagnostics.Debug.WriteLine(wallPerm1.Add);
                wallPerm1.Add = true; wallPerm1.AddEmployees = true; wallPerm1.Delete = true; wallPerm1.DeleteEmployees = true;  wallPerm1.DisplayFivePointQuestionUser = true;  wallPerm1.Observe = true; wallPerm1.Update = true;
                okrPer1.Add = true; okrPer1.AddEmployees = true; okrPer1.Delete = true; okrPer1.DeleteEmployees = true; okrPer1.Observe = true; okrPer1.Update = true; okrPer1.ObserveKResult = true; okrPer1.updateKResult = true; okrPer1.DeleteKResult = true; okrPer1.AddKResult = true;
                orgPerm1.AddEntities = true; orgPerm1.ChangeOrganizationProfile = true; orgPerm1.ChangeSettings = true;
                notifPerm1.GetNotification = true;
                await _wallRepository.Update(wallPerm1);
                await _okrRepository.Update(okrPer1);
                await _organizationRepository.Update(orgPerm1);
                await _notificationRepository.Update(notifPerm1);
                foreach (int i in childs)   // we make the journey on all the  chids of the entity to make the newuser as a manager also at thoses entities  
                {
                    Entity ii = await _entityRepository.Get(i);
                    var entityuserchild = _userEntityRepository.GetByUserId(input.NewMemberId, i);
                    entityuserchild.Role = input.NewRole;
                    await _userEntityRepository.Update(entityuserchild);
                    var wallPerm=_wallRepository.GetByUserEntityId(Newuser.Id, i);
                    var okrPer =_okrRepository.GetByUserEntityId(Newuser.Id, i);
                    var orgPerm = _organizationRepository.GetOrgPermission(Newuser.Id, i);
                    var notifPerm = _notificationRepository.GetByUserEntityId(Newuser.Id, i);
                    wallPerm.Add = true; wallPerm.AddEmployees = true; wallPerm.Delete = true; wallPerm.DeleteEmployees = true; wallPerm.DisplayFivePointQuestionUser = true; wallPerm.Observe = true; wallPerm.Update = true;
                    okrPer.Add = true; okrPer.AddEmployees = true; okrPer.Delete = true; okrPer.DeleteEmployees = true; okrPer.Observe = true; okrPer.Update = true; okrPer.ObserveKResult = true; okrPer.updateKResult = true; okrPer.DeleteKResult = true; okrPer.AddKResult = true;
                    orgPerm.AddEntities = true; orgPerm.ChangeOrganizationProfile = true; orgPerm.ChangeSettings = true;
                    notifPerm.GetNotification = true;
                    await _wallRepository.Update(wallPerm);
                    await _okrRepository.Update(okrPer);
                    await _organizationRepository.Update(orgPerm);
                    await _notificationRepository.Update(notifPerm);
                    await context.SaveChangesAsync();
                }
                Console.WriteLine("Creating Manager:" + New.UserId);
                return CreatedAtAction("Get", NewChange);
            }
            return CreatedAtAction("Get", userEntity);


        }

      //  [Authorize]
        [HttpPost("InviteMember")]
        public async Task<ActionResult<User_Entity>> PostMember([FromBody] InviteViewModel input)
        {

            var user = _userEntityRepository.GetByUserId(input.InviterId, input.EntityId);
            var Newuser = _userEntityRepository.GetByUserId(input.NewMemberId, input.EntityId);
            User New = await _userRepository.Get(input.NewMemberId);
            Entity ent = await _entityRepository.Get(input.EntityId);
            var userEntity = new User_Entity(New, ent, Role.Member);
            Boolean admin = false;
            if (user.Role == Role.Manager)
            {
                admin = true;
            }
            var childEntity = _childRepository.GetByParentId(input.EntityId);
            List<int> childs = new List<int>();
            foreach (ChildEntity ee in childEntity)
            {
                int childId = ee.ChildId;
                childs.Add(childId);
            }
            if( admin)
            {
                if (Newuser is null)
                {
                        if (input.NewRole == Role.Member)
                        {
                            foreach (int i in childs)
                            {
                                Entity ii = await _entityRepository.Get(i);
                                var childuserEnt = new User_Entity(New, ii, Role.Member);
                                var childwallPerm = new WallPermission(input.NewMemberId, i, true, true, true, true, false, false, true);
                                var childokrPerm = new OKRPermission(input.NewMemberId, i, true, true, true, true, false, false, false, false, false, false);
                                var childorgPerm = new OrganizationPermission(input.NewMemberId, i, true, true, true);
                                var childnotifPerm = new NotificationPermission(input.NewMemberId, i, true);
                                await _wallRepository.Add(childwallPerm);
                                await _okrRepository.Add(childokrPerm);
                                await _organizationRepository.Add(childorgPerm);
                                await _notificationRepository.Add(childnotifPerm);
                                await _userEntityRepository.Add(childuserEnt);

                            }
                            var wallPerm = new WallPermission(input.NewMemberId, input.EntityId, true, true, true, true, false, false, true);
                            var okrPerm = new OKRPermission(input.NewMemberId, input.EntityId, true, true, true, true, false, false, false, false, false, false);
                            var orgPerm = new OrganizationPermission(input.NewMemberId, input.EntityId, true, true, true);
                            var notifPerm = new NotificationPermission(input.NewMemberId, input.EntityId, true);
                            await _wallRepository.Add(wallPerm);
                            await _okrRepository.Add(okrPerm);
                            await _organizationRepository.Add(orgPerm);
                            await _notificationRepository.Add(notifPerm);
                            await _userEntityRepository.Add(userEntity);
                        Console.WriteLine("Creating Member:" + New.UserId);
                    }
                    
                    return CreatedAtAction("Get", userEntity);
                }
                else if (Newuser.Role == Role.Manager)
                {
                    return Ok("this user is Manager ");
                }
                else
                {
                    Newuser.Role = input.NewRole;
                    User_Entity NewChange = await _userEntityRepository.Update(Newuser);
                    foreach (int i in childs)   // we make the journey on all the  chids of the entity to make the newuser as a manager also at thoses entities  
                    {
                        Entity ii = await _entityRepository.Get(i);
                        var entityuserchild = _userEntityRepository.GetByUserId(input.NewMemberId, i);
                        entityuserchild.Role = input.NewRole;
                        await _userEntityRepository.Update(entityuserchild);
                        var wallPerm = _wallRepository.GetByUserEntityId(Newuser.Id, i);
                        var okrPer = _okrRepository.GetByUserEntityId(Newuser.Id, i);
                        var orgPerm = _organizationRepository.GetOrgPermission(Newuser.Id, i);
                        var notifPerm = _notificationRepository.GetByUserEntityId(Newuser.Id, i);
                        wallPerm.Add = true; wallPerm.AddEmployees = false; wallPerm.Delete = true; wallPerm.DeleteEmployees = false; wallPerm.DisplayFivePointQuestionUser = true; wallPerm.Observe = true; wallPerm.Update = true;
                        okrPer.Add = true; okrPer.AddEmployees = false; okrPer.Delete = true; okrPer.DeleteEmployees = false; okrPer.Observe = true; okrPer.Update = true; okrPer.ObserveKResult = true; okrPer.updateKResult = false; okrPer.DeleteKResult = false; okrPer.AddKResult = false;
                        orgPerm.AddEntities = false; orgPerm.ChangeOrganizationProfile = false; orgPerm.ChangeSettings = false;
                        notifPerm.GetNotification = true;
                        await _wallRepository.Update(wallPerm);
                        await _okrRepository.Update(okrPer);
                        await _organizationRepository.Update(orgPerm);
                        await _notificationRepository.Update(notifPerm);
                        Console.WriteLine("Creating Member:" + New.UserId);
                    }
                    return CreatedAtAction("Get", NewChange);
                }
            }
            else
            {
                return Ok("the inviter is not a manager for the entity  ");
            }
        }


       // [Authorize]
        [HttpPost("InviteObserver")]
        public async Task<ActionResult<User_Entity>> PostObserver([FromBody] InviteViewModel input)
        {

            var user = _userEntityRepository.GetByUserId(input.InviterId, input.EntityId);
            var Newuser = _userEntityRepository.GetByUserId(input.NewMemberId, input.EntityId);
            User New = await _userRepository.Get(input.NewMemberId);
            Entity ent = await _entityRepository.Get(input.EntityId);
            var userEntity = new User_Entity(New, ent, Role.Observer);
            Boolean admin = false;
            if(!(user is null)) {
                if (user.Role == Role.Manager)
                {
                    admin = true;
                }
            }
           
            
            if (admin)
            {
                if (Newuser is null)
                {
                    if (input.NewRole == Role.Observer)
                    {
                        var wallPerm = new WallPermission(input.NewMemberId, input.EntityId, false, false, false, true, false, false, false);
                        var okrPerm = new OKRPermission(input.NewMemberId, input.EntityId, false, false, false, true, false, false, false, false, false, false);
                        //var orgPerm = new OrganizationPermission(input.NewMemberId, input.EntityId, true, true, true);
                        var notifPerm = new NotificationPermission(input.NewMemberId, input.EntityId, true);
                        await _wallRepository.Add(wallPerm);
                        await _okrRepository.Add(okrPerm);
                        //await _organizationRepository.Add(orgPerm);
                        await _notificationRepository.Add(notifPerm);
                        await _userEntityRepository.Add(userEntity);
                        Console.WriteLine("Creating Observer:" + New.UserId);
                    }

                    return CreatedAtAction("Get", userEntity);
                }
                else if (Newuser.Role == Role.Manager)
                {
                    return Ok("this user is Manager ");
                }
                else
                {
                    Newuser.Role = input.NewRole;
                    User_Entity NewChange = await _userEntityRepository.Update(Newuser);
                    Console.WriteLine("Creating Observer:" + New.UserId);
                    return CreatedAtAction("Get", NewChange);
                }
            }
            else
            {
                return Ok("the inviter is not a manager for the entity  ");
            }
        }

        // DELETE: api/[controller]/5
      //  [Authorize]
        [HttpDelete("{entityId}/{UserId}/{invId}")]
        public async Task<ActionResult<User>> DeleteUserFromEntity(int entityId, int UserId,int invId)
        {
            var user = _userEntityRepository.GetByUserId(invId, entityId);
            Console.WriteLine(user.Role);
            var Newuser = _userEntityRepository.GetByUserId(UserId, entityId);
            Boolean admin = false;
            if (user.Role == Role.Manager)
            {
                admin = true;
            }
            if (admin)
            {
                if (!(Newuser is null))
                {
                    if (Newuser.Role != Role.Manager)
                    {
                        var user_entity = _userEntityRepository.GetByUserId(UserId, entityId);
                        var wall = _wallRepository.GetByUserEntityId(UserId, entityId);
                        var okr = _okrRepository.GetByUserEntityId(UserId, entityId);
                        var org = _organizationRepository.GetOrgPermission(UserId, entityId);
                        var notif = _notificationRepository.GetByUserEntityId(UserId, entityId);
                        _userEntityRepository.Remove(user_entity.Id);
                        await _wallRepository.Delete(wall.Id);
                        await _okrRepository.Delete(okr.Id);
                        if(!(org is null))
                        {
                            await _organizationRepository.Delete(org.Id);
                        }                       
                        await _notificationRepository.Delete(notif.Id);
                        return Ok("deleted");
                    }
                    else
                    {
                        return Ok("you can't delete a manager for the entity");
                    }

                }
                else
                {
                    return Ok("this user does not have a role for this entity");
                }
                
            }
            else
            {
                return Ok("you are not an admin for this entity");

            }

        }

    }
}

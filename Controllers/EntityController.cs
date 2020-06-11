using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using Organization.API.Services;
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
    public class EntityController : Controller
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUser_EntityRepository _userEntityRepository;
        private readonly IChildEntityRepository _childEntityRepository;
       // private readonly IAffectedPermissionRepository _permissionRepository;
        private readonly IWallPermissionRepository _wallRepository;
        private readonly IOkrPermissionRepository _okrRepository;
        private readonly INotificationPermissionRepository _notificationRepository;
        private readonly IOrganizationPermissionRepository _organizationRepository;


        public EntityController(IOrganizationPermissionRepository organizationRepository, INotificationPermissionRepository notificationRepository, IOkrPermissionRepository okrRepository, IWallPermissionRepository wallRepository, IEntityRepository baseRepository, IUserRepository userRepository, IUser_EntityRepository userEntityRepository, IChildEntityRepository childEntityRepository)
        {
            _entityRepository = baseRepository;
            _userRepository = userRepository;
            _userEntityRepository = userEntityRepository;
            _childEntityRepository = childEntityRepository;
            //_permissionRepository = permissionRepository;
            _wallRepository = wallRepository;
            _okrRepository = okrRepository;
            _notificationRepository = notificationRepository;
            _organizationRepository = organizationRepository;

        }


        // GET: api/[controller]
     //   [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entity>>> Get()
        {
            return await _entityRepository.GetAll();
        }


        // GET: api/[controller]/5
      //  [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Entity>> Get(int id)
        {
            var movie = await _entityRepository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
       // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Entity input)
        {
            Entity entity = await _entityRepository.Get(id);
            entity.Name = input.Name;entity.Description = input.Description;entity.Address = input.Address;
            await _entityRepository.Update(entity);
            
            return Ok(entity);
        }



       // [Authorize]
        [HttpPost]
        public async Task<ActionResult<Entity>> Post([FromBody] EntityViewModel input)
        {
            var userEnt = _userEntityRepository.GetByUserId(input.creator, input.ParentEntityId);
            System.Diagnostics.Debug.WriteLine(input.creator);
            var Newentity = new Entity(input.Name, input.Description, input.adresse);
            Newentity.Parent_id = input.ParentEntityId;
            //User user = await _userRepository.Get(input.creator);

            //var userParentEnt = _userEntityRepository.GetByUserId(input.creator, input.ParentEntityId);
            var userParentEnt = _userEntityRepository.GetByUserId(input.creator, input.ParentEntityId);
            if (input.ParentEntityId != 0 && !(userParentEnt is null)) ///  the parent 3andha manager houwa il creator && parentid mawjouda fil input 
            {

                await _entityRepository.Add(Newentity);
                Console.WriteLine("Creating new entity:" + Newentity.Name);
                System.Diagnostics.Debug.WriteLine("parent id exist ");
                System.Diagnostics.Debug.WriteLine((userParentEnt is null));
                System.Diagnostics.Debug.WriteLine((userParentEnt.Role ));
                if (userParentEnt.Role == 0 )// the creator is a manager for the parentEntity
                {
                    System.Diagnostics.Debug.WriteLine("the user is a manager for the entity parent ");
                    var wallPerm = new WallPermission(input.creator, Newentity.Id, true, true, true, true, true, true, true);
                    var okrPerm = new OKRPermission(input.creator, Newentity.Id, true, true, true, true, true, true, true, true, true, true);
                    var orgPerm = new OrganizationPermission(input.creator, Newentity.Id, true, true, true);
                    var notifPerm = new NotificationPermission(input.creator, Newentity.Id, true);
                    await _wallRepository.Add(wallPerm);
                    await _okrRepository.Add(okrPerm);
                    await _organizationRepository.Add(orgPerm);
                    await _notificationRepository.Add(notifPerm);
                    User user = await _userRepository.Get(input.creator);
                    await _userEntityRepository.Add(new User_Entity(user, Newentity, Role.Manager));    // il creator devient manager lil entity input 
                    Console.WriteLine("Creating Manager for the new entity:" + Newentity.Name);
                    var cc = _userEntityRepository.GetAdminsOfEntity(input.ParentEntityId); // nlawwjou 3al managers mta3 il parent entity 
                    foreach (User_Entity i in cc)
                    {
                        if (i.UserId != input.creator)
                        {
                            System.Diagnostics.Debug.WriteLine("the admin of the parent entity");
                            System.Diagnostics.Debug.WriteLine(i.UserId);
                            User usr = await _userRepository.Get(i.UserId);  /// il admin mta3 il parent 
                            if (_userEntityRepository.GetByUserId(usr.Id, Newentity.Id) is null)
                            {
                                await _userEntityRepository.Add(new User_Entity(usr, Newentity, Role.Manager));  // il manager mta3 il parent ywalliw manager lil entity zeda
                                Console.WriteLine("Creating Manager for the new entity:" + usr.Id);
                            }
                            var ParentwallPerm = new WallPermission(i.UserId, Newentity.Id, true, true, true, true, true, true, true);
                            var ParentokrPerm = new OKRPermission(i.UserId, Newentity.Id, true, true, true, true, true, true, true, true, true, true);
                            var ParentorgPerm = new OrganizationPermission(i.UserId, Newentity.Id, true, true, true);
                            var ParentnotifPerm = new NotificationPermission(i.UserId, Newentity.Id, true);
                            await _wallRepository.Add(ParentwallPerm);
                            await _okrRepository.Add(ParentokrPerm);
                            await _organizationRepository.Add(ParentorgPerm);
                            await _notificationRepository.Add(ParentnotifPerm);
                        }
                    }
                    var parent = _entityRepository.Get(input.ParentEntityId);  // il entity parent 
                    await _childEntityRepository.Add(new ChildEntity(parent.Result, Newentity));   // nzidou lil entity parent il entity as a child 
                          return Ok(Newentity);
                }
                else if (userParentEnt.Role != 0 )  // louken il parent mawjouda ama il creator mta3 il child mouch manager fil parent 
                 {
                         System.Diagnostics.Debug.WriteLine("the creator is not a manager for the entity parent ");
                       return Ok("the creator is not a manager for the parent Entity");
                  }
              }
              else if( input.ParentEntityId == 0)
              {
                await _entityRepository.Add(Newentity);
                //await this._busClient.PublishAsync(new EntityCreated(Newentity.Name, Newentity.Id.ToString()));
                //Console.WriteLine("Creating new entity:" + Newentity.Name);
                System.Diagnostics.Debug.WriteLine("there is no parent here ");
                  var wallPerm = new WallPermission(input.creator, Newentity.Id, true, true, true, true, true, true, true);
                  var okrPerm = new OKRPermission(input.creator, Newentity.Id, true, true, true, true, true, true, true, true, true, true);
                  var orgPerm = new OrganizationPermission(input.creator, Newentity.Id, true, true, true);
                  var notifPerm = new NotificationPermission(input.creator, Newentity.Id, true);
                  await _wallRepository.Add(wallPerm);
                  await _okrRepository.Add(okrPerm);
                  await _organizationRepository.Add(orgPerm);
                  await _notificationRepository.Add(notifPerm);
                  User user = await _userRepository.Get(input.creator);
                  await _userEntityRepository.Add(new User_Entity(user, Newentity, Role.Manager));
                //addeed last
                Console.WriteLine("Creating Manager for the new entity:" + Newentity.Name);
                return Ok(Newentity);
              }
              return Ok("there is no relation between the creator and the entity parent ");
          }



        // DELETE: api/[controller]/5
       // [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entity>> Delete(int id)
        {
            var movie = await _entityRepository.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

    }
}

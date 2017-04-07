using System.Collections.Generic;
using System.Linq;
using Discord;

namespace UnderBot.Utils {
    public class Role {
        public enum Roles {
            Everyone = 0,
            Nightbot = 1,
            Supporter = 2,
            YouTuber = 3,
            Mod = 3,
            Admin = 5
        }

        public static List<Roles> GetRoles(IGuildUser user) {
            var roles = new List<Roles>();
            foreach (var roleId in user.RoleIds) {
                var role = user.Guild.GetRole(roleId);
                switch (role.Position) {
                    case 1:
                        roles.Add(Roles.Nightbot);
                        break;
                    case 2:
                        roles.Add(Roles.Supporter);
                        break;
                    case 3:
                        roles.Add(role.Name == "Mod" ? Roles.Mod : Roles.YouTuber);
                        break;
                    default:
                        roles.Add(role.Position >= 5 ? Roles.Admin : Roles.Everyone);
                        break;
                }
            }
            return roles;
        }

        public static Roles GetHighestRole(IGuildUser user) {
            var roles = Roles.Everyone;
            foreach (var role in GetRoles(user)) {
                if ((int) role > (int)roles) {
                    roles = role;
                }
            }
            return roles;
        }

        public static Roles StringToRole(string role) {
            switch (role.ToLower()) {
                case "admin":
                    return Roles.Admin;
                case "mod":
                    return Roles.Mod;
                case "nightbot":
                    return Roles.Nightbot;
                case "supporter":
                    return Roles.Supporter;
                case "youtuber":
                    return Roles.YouTuber;
                default:
                    return Roles.Everyone;
            }
        }
    }
}
using DataRepository;
using System.Drawing;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class CharacterRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Character]"), ICharacterRepository
    {
        public void AddCharacter(CharacterDbo character)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (Username, FaceId, Color, XP)"
                      + $"VALUES("
                      + $"  @username, @faceId, @color, @xp);";
            var parameter = new
            {
                username = character.Username,
                faceId = character.FaceId,
                color = character.Color,
                xp = character.XP,
            };

            this.Insert<CharacterDbo>(sql, parameter);
        }

        public CharacterDbo? GetCharacterForUser(string username)
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE Username = @username";
            var parameters = new
            {
                username,
            };

            var result = this.Query<CharacterDbo>(sql, parameters).FirstOrDefault();
            return result;
        }

        public void UpdateFace(string username, string faceId)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $" SET FaceId = @faceId"
                      + $" WHERE Username = @username; ";

            var parameter = new
            {
                faceId = faceId,
                username = username,
            };

            this.Execute<CharacterDbo>(sql,
                              parameter);
        }

        public void UpdateColor(string username, string color)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $" SET Color = @color"
                      + $" WHERE Username = @username; ";

            var parameter = new
            {
                color = color,
                username = username,
            };

            this.Execute<CharacterDbo>(sql,
                                       parameter);
        }
    }
}

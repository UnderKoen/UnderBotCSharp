using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using MediaToolkit;
//using MediaToolkit.Model;
using YoutubeExplode;
using YoutubeExplode.Models;

//using VideoLibrary;

namespace UnderBot.Utils {
	public class Youtube {
		public static string GetTimLivestream() {
			string html;
			//tim
			const string url =
			    @"https://www.googleapis.com/youtube/v3/search?part=id&channelId=UC8cgXXpepeB2CWfxdy7uGVg&eventType=live&type=video&key=AIzaSyArRyAVT-C7T1d52sjiiPexPNdjyTzt9nU";
			//andere kut beest
			//const string url =
			//@"https://www.googleapis.com/youtube/v3/search?part=id&channelId=UClnQCgFa9lCBL-KXZMOoO9Q&eventType=live&type=video&key=AIzaSyArRyAVT-C7T1d52sjiiPexPNdjyTzt9nU";

			var request = (HttpWebRequest) WebRequest.Create(url);
			//request..AutomaticDecompression = DecompressionMethods.GZip;

			var response = (HttpWebResponse) null;

			try {
				response = request.GetResponseAsync().Result as HttpWebResponse;
			} catch (Exception e) {
				Console.WriteLine("[No double message]");
				Console.WriteLine(e.Message);
				return null;
			}
			var stream = response.GetResponseStream();
			var reader = new StreamReader(stream);
			{
				html = reader.ReadToEnd();
			}
			//Console.WriteLine(html);
			var pattern = new Regex("\"videoId\": \".*\"");
			var pattern2 = new Regex("\"videoId\": \"");
			html = pattern.Match(html).ToString();
			html = pattern2.Replace(html, "");
			html = html.Replace("\"", "");
			return html;
		}

		public static async Task<VideoInfo> GetVideoInfo(string link) {
			var name = link.Split('=')[1];
			var client = new YoutubeClient();
			var videoInfo = await client.GetVideoInfoAsync(name);
			return videoInfo;
		}

		public static TimeSpan GetVideoLenght(VideoInfo videoInfo) {
			return videoInfo.Duration;
		}

		public static string GetVideoTitle(VideoInfo videoInfo) {
			return videoInfo.Title;
		}

		public static string GetVideoUrl(VideoInfo videoInfo) {
			return "https://www.youtube.com/watch?v=" + videoInfo.Id;
		}

		public static async Task<List<string>> GetRandomUrls(string playlistLink, int amount) {
			var name = playlistLink.Split('=')[1];
			var client = new YoutubeClient();
			var playlistInfo = await client.GetPlaylistInfoAsync(name);
			var videoIds = playlistInfo.VideoIds;
			if (amount > videoIds.Count) {
				return null;
			}
			var list = new List<string>();
			for (var i = 0; i < amount; i++) {
				var e = videoIds[new Random().Next(0, videoIds.Count - 1)];
				while (list.Contains(e)) {
					e = videoIds[new Random().Next(0, videoIds.Count - 1)];
				}
				list.Add(e);
			}
			return list;
		}

		/*public static async Task<string> DownloadMp3(VideoInfo videoInfo) {
		    try {
			var client = new YoutubeClient();
			var name = videoInfo.Id + ".mp4";
			var streamInfo = videoInfo.Streams
			    .Where(s => s.ContainsVideo && s.ContainsAudio && s.FileExtension == "mp4" &&
					s.Quality == MediaStreamVideoQuality.Medium360)
			    .OrderBy(s => s.Quality);
			foreach (var info in streamInfo) {
			    using (var input = await client.GetMediaStreamAsync(info))
			    using (var output = File.Create(Path.Combine(Environment.CurrentDirectory, name)))
				await input.CopyToAsync(output);
			}

			var inputFile = new MediaFile {Filename = name};
			name = name.Replace(".mp4", ".mp3");
			var outputFile = new MediaFile {Filename = name};

			using (var engine = new Engine()) {
			    engine.GetMetadata(inputFile);

			    engine.Convert(inputFile, outputFile);
			}
			File.Delete(Path.Combine(Environment.CurrentDirectory, name.Replace(".mp3", ".mp4")));
			return name;
		    } catch (Exception e) {
			Console.WriteLine("Something went wrong with downloading music at: " + DateTime.Now);
			return null;
		    }
		}*/
	}
}
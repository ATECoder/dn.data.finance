# Quick Start Deployment Guide

## 🚀 Fastest Deployment: Azure Static Web Apps (Recommended)

### 5-Minute Setup

1. **Push to GitHub** (if not already done)
   ```powershell
   cd C:\my\lib\vs\data\finance
   git add .
   git commit -m "Add WebAssembly deployment files"
   git push
   ```

2. **Create Azure Static Web App**
   - Visit: https://portal.azure.com
   - Click "Create a resource" 
   - Search: "Static Web App"
   - Click Create
   - Fill form:
     - **Subscription**: Your Azure subscription
     - **Resource Group**: Create new or select existing
     - **Name**: `sep-ira-calculator` (or your preference)
     - **Region**: Select closest to users
     - **Hosting plan**: Free
   - Click "Sign in with GitHub"
   - **Organization**: ATECoder
   - **Repository**: dn.data.finance
   - **Branch**: main
   - **Build Presets**: .NET (Isolated)
   - **App location**: `src/sep.ira/calculator.wasm`
   - **API location**: (leave empty - no API)
   - **Output location**: `publish/wwwroot`
   - Click "Review + Create" → "Create"

3. **Wait for Deployment**
   - Azure creates GitHub Actions workflow automatically
   - First build takes 5-10 minutes
   - Workflow file: `.github/workflows/azure-static-web-apps-{name}.yml`

4. **Access Your App**
   - Deployment status appears on Azure Portal
   - URL: `https://sep-ira-calculator.azurestaticapps.net`
   - Share this URL with users!

### ✅ Your App is Live!

---

## 🐳 Alternative: Docker Deployment

### Build Docker Image
```powershell
# From repo root
docker build -t sep-ira-calculator:latest -f src/sep.ira/calculator.wasm/Dockerfile .

# Test locally
docker run -p 8080:80 sep-ira-calculator:latest
# Access: http://localhost:8080
```

### Push to Docker Hub
```powershell
# Login to Docker Hub
docker login

# Tag image
docker tag sep-ira-calculator:latest YOUR_DOCKERHUB_USERNAME/sep-ira-calculator:latest

# Push
docker push YOUR_DOCKERHUB_USERNAME/sep-ira-calculator:latest
```

### Deploy to Container Registry
- **Azure Container Registry (ACR)**: `az acr build --registry myregistry --image sep-ira:latest .`
- **Docker Hub**: Push and deploy to any container host
- **Cloud Run** (GCP): `gcloud run deploy`

---

## 📊 Deployment Comparison

### Azure Static Web Apps (RECOMMENDED)
```
✅ Pros:
  - FREE tier (5 free apps)
  - Global CDN included
  - Auto CI/CD from GitHub
  - Custom domains supported
  - Authentication ready
  - Staging environments

⚠️ Cons:
  - Limited to static sites
  - Regional limitations for free tier

💰 Cost: $0/month (free tier) - $99/month (standard)
```

### GitHub Pages
```
✅ Pros:
  - Free
  - Easy setup
  - GitHub integration

⚠️ Cons:
  - Limited customization
  - Public only
  - CNAME only for custom domains

💰 Cost: $0/month (completely free)
```

### Docker Container
```
✅ Pros:
  - Portable
  - Works anywhere
  - Full control

⚠️ Cons:
  - Need container host
  - More configuration
  - Requires infrastructure

💰 Cost: Varies ($0 - $100+/month depending on host)
```

---

## 🔐 Post-Deployment Checklist

- [ ] App loads without errors
- [ ] Calculator produces correct results
- [ ] Error handling works (test with invalid input)
- [ ] Responsive on mobile (test with browser DevTools)
- [ ] HTTPS is enforced
- [ ] Custom domain configured (optional)
- [ ] Error logs/monitoring enabled
- [ ] Documentation updated
- [ ] Users notified of new URL

---

## 🆘 Troubleshooting

### App shows blank page
```powershell
# 1. Check browser console (F12)
# 2. Verify in wwwroot folder exists
# 3. Check staticwebapp.config.json is present
# 4. View Azure deployment logs
```

### "404 Not Found" on refresh
```
This is normal for SPAs. Should be automatically fixed by:
- staticwebapp.config.json (Azure)
- URL Rewrite rules (IIS)
- GitHub Pages config
```

### WASM file not loading
```
1. Verify .wasm MIME type is "application/wasm"
2. Check Azure Static Web Apps settings
3. Enable gzip compression
4. Check browser console for specific errors
```

---

## 📞 Support Resources

- **Azure Docs**: https://docs.microsoft.com/azure/static-web-apps/
- **Blazor WebAssembly**: https://learn.microsoft.com/en-us/aspnet/core/blazor/
- **GitHub Actions**: https://docs.github.com/en/actions
- **Docker**: https://docs.docker.com/

---

## 🎯 Next Steps

1. Choose deployment method (Azure Static Web Apps recommended)
2. Follow deployment steps above
3. Test thoroughly in production
4. Monitor application performance
5. Gather user feedback
6. Plan regular updates

**Happy Deploying! 🚀**

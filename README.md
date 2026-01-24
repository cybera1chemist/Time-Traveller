# Time-traveler

## 使用方法

### 第一次使用

1. 把此仓库clone到本地；
2. 打开Unity Hub，并点击右上角的Open；
3. 选择此仓库的根目录即可。

### 分支管理

* 禁止在 `main` 上开发或 push，日常开发全部基于 `dev`

* 每个人写自己的功能：一个功能 = 一个 feat 分支

* 合并一律从 feat-xxx 合并到 dev

* 避免多人同时修改同一个场景 / 同一个 Prefab，防止互相伤害

#### Step 1：开发前更新当前分支
```bash
git checkout dev
git pull
```

#### Step 2：创建/切换自己的功能分支
```bash
git checkout -b feat-功能名
# 示例
git checkout -b feat-player-attack
```

### Step 3：
```bash
git add .
git commit -m "feat: 我这次都做了些什么工作"
git push --set-upstream origin feat-功能名
```

### Step 4：觉得没问题就合并到`dev`分支
```bash
git checkout dev
git pull
git merge feat-功能名
git push
```

### Step 5（可选）：删除不要的分支
```bash
git branch -d feat-功能名
git push origin --delete feat-功能名
```

## 注意事项

1. 本项目使用的 Unity Editor Version 是 `2022.3.62f2c1`，大家最好都使用同样的版本，以免出现不必要的bug。
